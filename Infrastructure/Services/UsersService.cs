using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Application.Interfaces.Services;
using Domain.Entities.Auth;
using Application.Queries.Users;
using Application.Commands.Users;
using Application.DTO;
using Application.Queries.Auth;
using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Domain.Enums;
using Domain.Service;

namespace Infrastructure.Services
{
    public class UsersService : IUsersService
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;
        private readonly ILogger logger;

        public UsersService(IMediator mediator, ILoggerFactory factory,
            IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
            logger = factory.CreateLogger<UsersService>();
        }

        public async Task<IEnumerable<User>> GetUsersAsync(string role)
        {
            IEnumerable<User> users;

            if (string.IsNullOrEmpty(role))
            {
                try
                {
                    users = await mediator.Send(new GetUsersQuery());
                }
                catch (Exception ex)
                {
                    logger.LogError
                        ("{ExString}: {Ex}", UserServiceStrings.GetUsersException, ex.Message);
                    throw new Exception(UserServiceStrings.GetUsersException);
                }
            }
            else
            {
                Roles? roleEnum = Enum.TryParse(role,true, out Roles result) ? result : null;
                if (roleEnum.HasValue)
                {
                    try
                    {
                        users = await mediator.Send(new GetUsersInRoleQuery(roleEnum.Value));
                    }
                    catch (Exception ex)
                    {
                        logger.LogError
                            ("{ExString}: {Ex}",UserServiceStrings.GetUsersException, ex.Message);
                        throw new Exception(UserServiceStrings.GetUsersException);
                    }
                }
                else
                {
                    logger.LogError(UserServiceStrings.GetUsersRoleException);
                    throw new ArgumentException(UserServiceStrings.GetUsersRoleException);
                }
            }
            
            if (users != null)
            {
                return users;
            }

            logger.LogError(UserServiceStrings.GetUsersNullException);
            throw new ArgumentException(UserServiceStrings.GetUsersNullException);
        }

        public async Task<UserDTO> GetUserByIdAsync(Guid id)
        {
            User user;
            try
            {
                user = await mediator.Send(new GetUserByIdQuery(id));
            }
            catch (Exception ex)
            {
                logger.LogError("{ExString}: {Ex}", UserServiceStrings.GetUserException, ex.Message);
                throw new Exception(UserServiceStrings.GetUserException);
            }

            if (user != null)
            {
                UserDTO userDto;
                try
                {
                    userDto = mapper.Map<UserDTO>(user);
                }
                catch (Exception ex)
                {
                    logger.LogError("{ExString}: {Ex}", UserServiceStrings.GetUserException, ex.Message);
                    throw new Exception(UserServiceStrings.GetUserException);
                }
                
                try
                {
                    userDto.Roles = await mediator.Send(new GetRolesByUserQuery(user));
                }
                catch (Exception ex)
                {
                    logger.LogError("{ExString}: {Ex}", UserServiceStrings.GetRolesException, ex.Message);
                    throw new Exception(UserServiceStrings.GetRolesException);
                }

                return userDto;
            }

            logger.LogError(UserServiceStrings.GetUserIdException);
            throw new ArgumentException(UserServiceStrings.GetUserIdException);
        }
        
        public async Task<UserDTO> GetUserByEmailAsync(string email)
        {
            User user;
            try
            {
                user = await mediator.Send(new GetUserByEmailQuery(email));
            }
            catch (Exception ex)
            {
                logger.LogError("{ExString}: {Ex}", UserServiceStrings.GetUserException, ex.Message);
                throw new Exception(UserServiceStrings.GetUserException);
            }

            if (user != null)
            {
                UserDTO userDto;
                try
                {
                    userDto = mapper.Map<UserDTO>(user);
                }
                catch (Exception ex)
                {
                    logger.LogError("{ExString}: {Ex}", UserServiceStrings.GetUserException, ex.Message);
                    throw new Exception(UserServiceStrings.GetUserException);
                }
                
                try
                {
                    userDto.Roles = await mediator.Send(new GetRolesByUserQuery(user));
                }
                catch (Exception ex)
                {
                    logger.LogError("{ExString}: {Ex}", UserServiceStrings.GetRolesException, ex.Message);
                    throw new Exception(UserServiceStrings.GetRolesException);
                }
                return userDto;
            }

            logger.LogError(UserServiceStrings.GetUserEmailException);
            throw new ArgumentException(UserServiceStrings.GetUserEmailException);
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
                logger.LogError("{ExString}: {Ex}", UserServiceStrings.DeleteUserException, ex.Message);
                throw new Exception(UserServiceStrings.DeleteUserException);
            }

            if (!deleteResult.Succeeded)
            {
                logger.LogError(UserServiceStrings.DeleteUserIdException);
                return false;
            }
            else
            {
                logger.LogInformation(UserServiceStrings.UserDeletedInformation);
                return true;
            }
        }

        public async Task<bool> DisableUserAsync(DisableUserModel model)
        {
            if (model.DisableEnd.HasValue && model.DisableEnd < DateTime.Now)
            {
                logger.LogError(UserServiceStrings.DisableUserTimeException);
                throw new ArgumentException(UserServiceStrings.DisableUserTimeException);
            }

            bool disableResult;
            try
            {
                disableResult = await mediator.Send(new DisableUserCommand(model.UserId, model.DisableEnd), default);
            }
            catch (Exception ex)
            {
                logger.LogError("{ExString}: {Ex}", UserServiceStrings.DisableUserException, ex);
                throw new Exception(UserServiceStrings.DisableUserException);
            }

            if (!disableResult)
            {
                logger.LogError(UserServiceStrings.DisableUserIdException);
                return false;
            }
            else
            {
                logger.LogInformation(UserServiceStrings.UserDisabledInformation);
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
                logger.LogError("{ExString}: {Ex}",UserServiceStrings.EnableUserException, ex.Message);
                throw new Exception(UserServiceStrings.EnableUserException);
            }

            if (!enableResult)
            {
                logger.LogError(UserServiceStrings.EnableUserIdException);
                return false;
            }
            else
            {
                logger.LogInformation(UserServiceStrings.UserEnabledInformation);
                return true;
            }
        }
        
        public async Task<bool> RemoveUserFromRoleAsync(RemoveUserFromRoleModel model,
            string currentEmail, CancellationToken token)
        {
            if (!Enum.TryParse(model.Role, true, out Roles role))
            {
                logger.LogError(UserServiceStrings.RemoveUserFromRoleExceptionRole);
                throw new Exception(UserServiceStrings.RemoveUserFromRoleExceptionRole);
            }
            
            User currentUser;
            try
            {
                currentUser = await mediator.Send(new GetUserByEmailQuery(currentEmail), token);
            }
            catch (Exception ex)
            {
                logger.LogError("{ExString}: {Ex}", UserServiceStrings.GetUserException, ex.Message);
                throw new Exception(UserServiceStrings.GetUserException);
            }

            IEnumerable<string> currentRoles;
            try
            {
                currentRoles = await mediator.Send(new GetRolesByUserQuery(currentUser), token);
            }
            catch (Exception ex)
            {
                logger.LogError("{ExString}: {Ex}", UserServiceStrings.GetRolesException, ex.Message);
                throw new Exception(UserServiceStrings.GetRolesException);
            }

            if (currentRoles.Contains(model.Role))
            {
                logger.LogError("{ExString}", UserServiceStrings.RemoveFromRoleCurrentRoleException);
                throw new Exception(UserServiceStrings.RemoveFromRoleCurrentRoleException);
            }
            
            bool removeResult;
            try
            {
                removeResult = await mediator.Send(
                    new RemoveUserFromRoleCommand(model.UserId, model.Role), token);
            }
            catch (Exception ex)
            {
                logger.LogError("{ExString}: {Ex}", UserServiceStrings.RemoveUserFromRoleException, ex);
                throw new Exception(UserServiceStrings.RemoveUserFromRoleException);
            }

            if (!removeResult)
            {
                logger.LogError(UserServiceStrings.RemoveUserFromRoleIdException);
                return false;
            }
            else
            {
                logger.LogInformation(UserServiceStrings.UserRemovedFromRoleInformation);
                return true;
            }
        }
    }
}