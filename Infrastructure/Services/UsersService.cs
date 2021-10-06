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
    public class UsersService : IUsersService
    {
        private readonly IMediator mediator;
        private readonly ILogger logger;

        public UsersService(IMediator mediator, ILoggerFactory factory)
        {
            this.mediator = mediator;
            logger = factory.CreateLogger<UsersService>();
        }

        public async Task<IEnumerable<User>> GetUsersAsync(string role)
        {
            IEnumerable<User> users;

            if (string.IsNullOrEmpty(role))
            {
                try
                {
                    users = await mediator.Send(new GetUsersQuery(), default);
                }
                catch (Exception ex)
                {
                    logger.LogError("Error occured while getting users: {Ex}", ex);
                    throw new Exception("An internal error occured while getting users");
                }
            }
            else
            {
                Roles? roleEnum = Enum.TryParse(role, out Roles result) ? result : null;
                if (roleEnum.HasValue)
                {
                    try
                    {
                        users = await mediator.Send(new GetUsersInRoleQuery(roleEnum.Value));
                    }
                    catch (Exception ex)
                    {
                        logger.LogError("Error occured while getting users: {Ex}", ex);
                        throw new Exception("An internal error occured while getting users");
                    }
                }
                else
                {
                    logger.LogError("Failed to get users. Role is incorrect");
                    throw new ArgumentException("Failed to get users. Role is incorrect");
                }
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
                user = await mediator.Send(new GetUserByIdQuery(id));
            }
            catch (Exception ex)
            {
                logger.LogError("Error occured while getting user: {Ex}", ex);
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
                deleteResult = await mediator.Send(new DeleteUserCommand(id));
            }
            catch (Exception ex)
            {
                logger.LogError("Error occured while deleting user: {Ex}", ex);
                throw new Exception("An internal error occured while deleting user");
            }

            if (!deleteResult.Succeeded)
            {
                logger.LogError("Failed to delete user. Wrong id");
                return false;
            }
            else
            {
                logger.LogInformation("User with {Id} deleted", id);
                return true;
            }
        }

        public async Task<bool> DisableUserAsync(DisableUserModel model)
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
                logger.LogError("Error occured while disabling user: {Ex}", ex);
                throw new Exception("An internal error occured while disabling user");
            }

            if (!disableResult)
            {
                logger.LogError("Failed to disable user. Wrong id");
                return false;
            }
            else
            {
                logger.LogInformation("User with {model.UserId} disabled", model.UserId);
                return true;
            }
        }

        public async Task<bool> EnableUserAsync(Guid id)
        {
            bool enableResult;
            try
            {
                enableResult = await mediator.Send(new EnableUserCommand(id));
            }
            catch (Exception ex)
            {
                logger.LogError("Error occured while enabling user: {Ex}", ex);
                throw new Exception("An internal error occured while enabling user");
            }

            if (!enableResult)
            {
                logger.LogError("Failed to enable user. Wrong id");
                return false;
            }
            else
            {
                logger.LogInformation("User with {Id} enabled", id);
                return true;
            }
        }
    }
}