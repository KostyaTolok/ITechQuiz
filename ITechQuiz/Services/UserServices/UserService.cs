using ITechQuiz.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using ITechQuiz.Services.UserServices.Commands;
using ITechQuiz.Services.UserServices.Queries;
using System.Collections.Generic;

namespace ITechQuiz.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly IMediator mediator;
        private readonly ILogger logger;

        public UserService(IMediator mediator, ILogger logger)
        {
            this.mediator = mediator;
            this.logger = logger;
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

            if (user != null)
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

        public async Task LogOut()
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
