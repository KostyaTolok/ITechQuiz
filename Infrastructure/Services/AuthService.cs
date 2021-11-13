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
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.Users;
using Domain.Service;

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

        public async Task<string> Login(LoginModel model)
        {
            SignInResult signInResult;
            try
            {
                signInResult =
                    await mediator.Send(new CheckPasswordSignInCommand(model.Email,
                        model.Password, model.RememberMe));
            }
            catch (Exception ex)
            {
                logger.LogError("{ExString}: {Ex}", AuthServiceStrings.LoginException, ex.Message);
                throw new Exception(AuthServiceStrings.LoginException);
            }

            if (signInResult.IsNotAllowed)
            {
                logger.LogError
                    ("{ExString} {Email}", AuthServiceStrings.UserDisabledException, model.Email);
                throw new ArgumentException(AuthServiceStrings.UserDisabledException);
            }
            else if (signInResult.IsLockedOut)
            {
                logger.LogError
                    ("{ExString} {Email}", AuthServiceStrings.UserDisabledException, model.Email);
                throw new ArgumentException(AuthServiceStrings.UserLockedOutException);
            }
            else if (!signInResult.Succeeded)
            {
                logger.LogError
                    ("{ExString} {Email}", AuthServiceStrings.UserSignInException, model.Email);
                throw new ArgumentException(AuthServiceStrings.UserLoginOrPasswordException);
            }
            else
            {
                logger.LogInformation
                    ("{ExString} {Email}", AuthServiceStrings.UserSignInInformation, model.Email);
                return await mediator.Send(new CreateTokenCommand(model.Email));
            }
        }

        public async Task<string> Register(RegisterModel model)
        {
            User user;

            try
            {
                user = await mediator.Send(new GetUserByEmailQuery(model.Email));
            }
            catch (Exception ex)
            {
                logger.LogError("{ExString}: {Ex}", AuthServiceStrings.RegisterException, ex.Message);
                throw new Exception(AuthServiceStrings.RegisterException);
            }

            if (user == null)
            {
                user = new User() {Email = model.Email, UserName = model.Name};

                IdentityResult registerResult;
                try
                {
                    registerResult = await mediator.Send(new CreateUserCommand(user,
                        model.Password));
                }
                catch (Exception ex)
                {
                    logger.LogError("{ExString}: {Ex}", AuthServiceStrings.RegisterException, ex.Message);
                    throw new Exception(AuthServiceStrings.RegisterException);
                }

                if (registerResult.Succeeded)
                {
                    try
                    {
                        logger.LogInformation(AuthServiceStrings.RegisterException);
                        return await mediator.Send(new CreateTokenCommand(model.Email));
                    }
                    catch (Exception ex)
                    {
                        logger.LogError
                            ("{ExString}: {Ex}", AuthServiceStrings.RegisterException, ex.Message);
                        throw new Exception(AuthServiceStrings.RegisterException);
                    }
                }
                else
                {
                    throw new ArgumentException(AuthServiceStrings.RegisterFailedException);
                }
            }
            else
            {
                throw new ArgumentException(AuthServiceStrings.UserExistsException);
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
                logger.LogError("{ExString}: {Ex}", AuthServiceStrings.LogoutException, ex.Message);
                throw new Exception(AuthServiceStrings.LogoutException);
            }
        }
        
        public async Task ChangePasswordAsync(ChangePasswordModel model,
            CancellationToken token)
        {
            User user;

            try
            {
                user = await mediator.Send(new GetUserByEmailQuery(model.Email), token);
            }
            catch (Exception ex)
            {
                logger.LogError("{ExString}: {Ex}", AuthServiceStrings.ChangePasswordException, ex.Message);
                throw new Exception(AuthServiceStrings.ChangePasswordException);
            }

            if (user == null)
            {
                logger.LogError(AuthServiceStrings.ChangePasswordExceptionUserNotFound);
                throw new ArgumentException(AuthServiceStrings.ChangePasswordExceptionUserNotFound);
            }
            
            IdentityResult changeResult;
            try
            {
                changeResult = await mediator.Send(
                    new ChangePasswordCommand(user, model.OldPassword,model.NewPassword), token);
            }
            catch (Exception ex)
            {
                logger.LogError("{ExString}: {Ex}", AuthServiceStrings.ChangePasswordException, ex.Message);
                throw new Exception(AuthServiceStrings.ChangePasswordException);
            }

            if (!changeResult.Succeeded)
            {
                logger.LogError(AuthServiceStrings.ChangePasswordExceptionMismatch);
                throw new ArgumentException(changeResult.Errors.First().Description);
            }
            else
            {
                logger.LogInformation(AuthServiceStrings.PasswordChangeInformation);
            }
        }
    }
}