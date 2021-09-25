using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Application.Interfaces.Services;
using Domain.Entities.Auth;
using Application.Queries.Users;
using Application.Commands.Users;
using Domain.Models;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IMediator mediator;
        private readonly ILogger logger;

        public UserService(IMediator mediator, ILoggerFactory factory)
        {
            this.mediator = mediator;
            logger = factory.CreateLogger<UserService>();
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            var users = await mediator.Send(new GetUsersQuery(), default);

            if (users != null)
            {
                return users;
            }

            logger.LogError("Failed to get users");
            throw new ArgumentException("Failed to get users");
        }

        public async Task<User> GetUserAsync(Guid id)
        {
            var user = await mediator.Send(new GetUserByIdQuery(id), default);

            if (user != null)
            {
                return user;
            }

            logger.LogError("Failed to get user. Wrong id");
            throw new ArgumentException("Failed to get user. Wrong id");
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var deleteResult = await mediator.Send(new DeleteUserCommand(id), default);

            if (!deleteResult.Succeeded)
            {
                logger.LogError("Failed to delete user. Wrong id");
                return false;
            }
            else
            {
                logger.LogInformation($"User with {id} deleted");
                return true;
            }
        }

        public async Task<bool> AddToRoleAsync(AddToRoleModel model)
        {
            var addToRoleResult = await mediator.Send(new AddToRoleCommand(model.UserId, model.Role), default);

            if (!addToRoleResult.Succeeded)
            {
                logger.LogError($"Failed to add user to {model.Role} role. Wrong id");
                return false;
            }
            else
            {
                logger.LogInformation($"User with {model.UserId} added to {model.Role} role");
                return true;
            }
        }

        public async Task<bool> DisableUserAsync(DisableModel model)
        {
            if (model.DisableEnd < DateTime.Now)
            {
                logger.LogError("Failed to disable user. Disable end time is incorrect");
                throw new ArgumentException("Failed to disable user. Disable end time is incorrect");
            }

            var disableResult = await mediator.Send(new DisableUserCommand(model.UserId, model.DisableEnd), default);

            if (!disableResult)
            {
                logger.LogError("Failed to disable user. Wrong id");
                return false;
            }
            else
            {
                logger.LogInformation($"User with {model.UserId} disabled");
                return true;
            }
        }

        public async Task<bool> EnableUserAsync(Guid id)
        {
            var disableResult = await mediator.Send(new EnableUserCommand(id), default);

            if (!disableResult)
            {
                logger.LogError("Failed to enable user. Wrong id");
                return false;
            }
            else
            {
                logger.LogInformation($"User with {id} enabled");
                return true;
            }
        }
    }
}
