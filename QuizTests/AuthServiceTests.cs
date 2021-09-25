using Application.Commands.Auth;
using Application.Interfaces.Services;
using Application.Queries.Auth;
using Domain.Entities.Auth;
using Domain.Models;
using Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTests
{
    public class AuthServiceTests
    {
        private readonly Mock<IMediator> mediator = new();
        private readonly User user = TestData.GetTestUsers()[0];

        [Fact]
        public async Task LoginUserTest()
        {
            mediator.Setup(m => m.Send(It.IsAny<PasswordSignInUserCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(SignInResult.Success)
                .Verifiable();

            IAuthService authService = new AuthService(mediator.Object, NullLoggerFactory.Instance);
            var loginModel = new LoginModel() { Email = user.Email, Password = "123456", RememberMe = true };
            await authService.Login(loginModel);

            mediator.Verify();
        }

        [Fact]
        public async Task LoginUserTestThrowsArgumentException()
        {
            mediator.Setup(m => m.Send(It.IsAny<PasswordSignInUserCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(SignInResult.Failed)
                .Verifiable();

            IAuthService surveyService = new AuthService(mediator.Object, NullLoggerFactory.Instance);
            var loginModel = new LoginModel() { Email = "", Password = "", RememberMe = true };

            ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(async () => await surveyService.Login(loginModel));

            mediator.Verify();

            Assert.Equal("Wrong login or password", exception.Message);
        }

        [Fact]
        public async Task RegisterUserTest()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetUserByEmailQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((User)null)
                .Verifiable();
            mediator.Setup(m => m.Send(It.IsAny<AddUserCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(IdentityResult.Success)
                .Verifiable();
            mediator.Setup(m => m.Send(It.IsAny<SignInUserCommand>(), It.IsAny<CancellationToken>()))
                .Verifiable();

            IAuthService authService = new AuthService(mediator.Object, NullLoggerFactory.Instance);
            var registerModel = new RegisterModel() { Email = user.Email, Password = "123456", Name = user.UserName };
            await authService.Register(registerModel);

            mediator.VerifyAll();
        }

        [Fact]
        public async Task RegisterUserTestThrowsArgumentExceptionUserExists()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetUserByEmailQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(user)
                .Verifiable();

            IAuthService authService = new AuthService(mediator.Object, NullLoggerFactory.Instance);
            var registerModel = new RegisterModel() { Email = user.Email, Password = "123456", Name = user.UserName };
            ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(async () => await authService.Register(registerModel));

            mediator.VerifyAll();
            Assert.Equal("User with this email already exists", exception.Message);
        }

        [Fact]
        public async Task LogoutUserTest()
        {
            mediator.Setup(m => m.Send(It.IsAny<SignOutUserCommand>(), It.IsAny<CancellationToken>()))
                .Verifiable();

            IAuthService authService = new AuthService(mediator.Object, NullLoggerFactory.Instance);
            await authService.Logout();

            mediator.Verify();
        }

    }
}
