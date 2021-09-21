using Application.Commands.Auth;
using Application.Interfaces.Services;
using Application.Queries.Auth;
using Domain.Entities.Auth;
using Domain.Models;
using MediatR;
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
            var signInResult = await mediator.Send(new PasswordSignInUserCommand(model.Email, model.Password, model.RememberMe), default);

            if (!signInResult.Succeeded)
            {
                throw new ArgumentException("Wrong login or password");
            }
            logger.LogInformation($"User {model.Email} signed in");
        }

        public async Task Register(RegisterModel model)
        {
            var user = await mediator.Send(new GetUserByEmailQuery(model.Email), default);

            if (user == null)
            {
                user = new User() { Email = model.Email, UserName = model.Name };

                var registerResult = await mediator.Send(new AddUserCommand(user, model.Password), default);

                if (registerResult.Succeeded)
                {
                    await mediator.Send(new SignInUserCommand(user), default);
                    logger.LogInformation($"User {user.UserName} registered");
                }
                else
                {
                    throw new ArgumentException("Failed to register");
                }
            }
            else
            {
                throw new ArgumentException("User with this email already exists");
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
                throw new Exception($"Failed to logout: {ex.Message}");
            }
        }
    }
}
