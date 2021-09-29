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
using Microsoft.AspNetCore.Identity;
using Domain.Enums;

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

        public async Task<IEnumerable<User>> GetUsersAsync(Roles? role)
        {
            IEnumerable<User> users;
            try
            {
                if (role.HasValue)
                {
                    users = await mediator.Send(new GetUsersInRoleQuery(role.Value), default);
                }
                else
                {
                    users = await mediator.Send(new GetUsersQuery(), default);
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Error occured while getting users: {ex}");
                throw new Exception("An internal error occured while getting users");
            }

            if (users != null)
            {
                return users;
            }

            logger.LogError("Failed to get users");
            throw new ArgumentException("Failed to get users");
        }

        public async Task<User> GetUserAsync(Guid id)
        {
            User user;
            try
            {
                user = await mediator.Send(new GetUserByIdQuery(id), default);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error occured while getting user: {ex}");
                throw new Exception("An internal error occured while getting user");
            }

            if (user != null)
            {
                return user;
            }

            logger.LogError("Failed to get user. Wrong id");
            throw new ArgumentException("Failed to get user. Wrong id");
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            IdentityResult deleteResult;
            try
            {
                 deleteResult = await mediator.Send(new DeleteUserCommand(id), default);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error occured while deleting user: {ex}");
                throw new Exception("An internal error occured while deleting user");
            }

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
            bool addToRoleResult;
            try
            {
                addToRoleResult = await mediator.Send(new AddToRoleCommand(model.UserId, model.Role), default);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error occured while adding user to role: {ex}");
                throw new Exception("An internal error occured while adding user to role");
            }

            if (!addToRoleResult)
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
            if (model.DisableEnd.HasValue && model.DisableEnd < DateTime.Now)
            {
                logger.LogError("Failed to disable user. Disable end time is incorrect");
                throw new ArgumentException("Failed to disable user. Disable end time is incorrect");
            }

            bool disableResult;
            try
            {
                disableResult = await mediator.Send(new DisableUserCommand(model.UserId, model.DisableEnd), default);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error occured while disabling user: {ex}");
                throw new Exception("An internal error occured while disabling user");
            }

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
            bool enableResult;
            try
            {
                enableResult = await mediator.Send(new EnableUserCommand(id), default);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error occured while enabling user: {ex}");
                throw new Exception("An internal error occured while enabling user");
            }

            if (!enableResult)
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
