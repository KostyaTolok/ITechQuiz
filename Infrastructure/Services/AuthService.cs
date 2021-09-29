using Application.Commands.Auth;
using Application.Interfaces.Services;
using Application.Queries.Auth;
using Domain.Entities.Auth;
using Domain.Exceptions;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IMediator mediator;
        private readonly ILogger logger;

        public AuthService(IMediator mediator, ILoggerFactory factory)
        {
            this.mediator = mediator;
            logger = factory.CreateLogger<AuthService>();
        }

        public async Task Login(LoginModel model)
        {
            SignInResult signInResult;
            try
            {
                signInResult = await mediator.Send(new PasswordSignInUserCommand(model.Email, model.Password, model.RememberMe), default);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error occured while logging: {ex}");
                throw new Exception("An internal error occured while signing in");
            }

            if (signInResult.IsNotAllowed)
            {
                logger.LogError($"User {model.Email} is disabled");
                throw new BusinessLogicException($"User {model.Email}  is disabled");
            }
            else if (signInResult.IsLockedOut)
            {
                logger.LogError($"User {model.Email} is locked out");
                throw new BusinessLogicException($"User {model.Email}  is locked out");
            }
            else if (!signInResult.Succeeded)
            {
                logger.LogError($"User {model.Email} failed to sign in");
                throw new BusinessLogicException("Wrong login or password");
            }

            logger.LogInformation($"User {model.Email} signed in");
        }

        public async Task Register(RegisterModel model)
        {
            User user;

            try
            {
                user = await mediator.Send(new GetUserByEmailQuery(model.Email), default);
            }
            catch (Exception ex)
            {
                logger.LogError($"Error occured while registration: {ex}");
                throw new Exception("An internal error occured while registration");
            }

            if (user == null)
            {
                user = new User() { Email = model.Email, UserName = model.Name };

                IdentityResult registerResult;
                try
                {
                    registerResult = await mediator.Send(new AddUserCommand(user, model.Password), default);
                }
                catch (Exception ex)
                {
                    logger.LogError($"Error occured while registration: {ex}");
                    throw new Exception("An internal error occured while registration");
                }

                if (registerResult.Succeeded)
                {
                    try
                    {
                        await mediator.Send(new SignInUserCommand(user), default);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError($"Error occured while registration: {ex}");
                        throw new Exception("An internal error occured while registration");
                    }

                    logger.LogInformation($"User {user.UserName} registered");
                }
                else
                {
                    throw new BusinessLogicException("Failed to register");
                }
            }
            else
            {
                throw new BusinessLogicException("User with this email already exists");
            }
        }

        public async Task Logout()
        {
            try
            {
                await mediator.Send(new SignOutUserCommand(), default);
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to logout: {ex}");
                throw new Exception($"Failed to logout");
            }
        }
    }
}
