using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Application.Interfaces.Services;
using Domain.Entities.Auth;
using Application.Queries.Users;
using Application.Commands.Users;

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

        public async Task DeleteUserAsync(Guid id)
        {
            var deleteResult = await mediator.Send(new DeleteUserCommand(id), default);

            if (!deleteResult.Succeeded)
            {
                logger.LogError("Failed to delete survey. Wrong id");
                throw new ArgumentException("Failed to delete survey. Wrong id");
            }
        }
    }
}
