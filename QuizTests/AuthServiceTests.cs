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
using Domain.Service;
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
            mediator.Setup(m => m.Send(It.IsAny<CheckPasswordSignInCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(SignInResult.Success)
                .Verifiable();

            IAuthService authService = new AuthService(mediator.Object, NullLoggerFactory.Instance);
            var loginModel = new LoginModel() { Email = user.Email, Password = "123456", RememberMe = true };
            await authService.Login(loginModel);

            mediator.VerifyAll();
        }
        
        [Fact]
        public async Task LoginUserDisabledTest()
        {
            mediator.Setup(m => m.Send(It.IsAny<CheckPasswordSignInCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(SignInResult.NotAllowed)
                .Verifiable();

            IAuthService authService = new AuthService(mediator.Object, NullLoggerFactory.Instance);
            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await authService.Login(new LoginModel()));

            mediator.VerifyAll();

            Assert.Equal(AuthServiceStrings.UserDisabledException, exception.Message);
        }
        
        [Fact]
        public async Task LoginUserLockedOutTest()
        {
            mediator.Setup(m => m.Send(It.IsAny<CheckPasswordSignInCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(SignInResult.LockedOut)
                .Verifiable();

            IAuthService authService = new AuthService(mediator.Object, NullLoggerFactory.Instance);
            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await authService.Login(new LoginModel()));

            mediator.VerifyAll();

            Assert.Equal(AuthServiceStrings.UserLockedOutException, exception.Message);
        }
        
        [Fact]
        public async Task LoginUserTestThrowsInnerException()
        {
            mediator.Setup(m => m.Send(It.IsAny<CheckPasswordSignInCommand>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception())
                .Verifiable();

            IAuthService authService = new AuthService(mediator.Object, NullLoggerFactory.Instance);

            var exception = await Assert.ThrowsAsync<Exception>(async () => await authService.Login(new LoginModel()));

            mediator.VerifyAll();

            Assert.Equal(AuthServiceStrings.LoginException, exception.Message);
        }
        
        [Fact]
        public async Task LoginUserTestThrowsLoginOrPasswordException()
        {
            mediator.Setup(m => m.Send(It.IsAny<CheckPasswordSignInCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(SignInResult.Failed)
                .Verifiable();

            IAuthService authService = new AuthService(mediator.Object, NullLoggerFactory.Instance);
            var loginModel = new LoginModel() { Email = "", Password = "", RememberMe = true };

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await authService.Login(loginModel));

            mediator.VerifyAll();

            Assert.Equal(AuthServiceStrings.UserLoginOrPasswordException, exception.Message);
        }

        [Fact]
        public async Task RegisterUserTest()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetUserByEmailQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((User)null)
                .Verifiable();
            mediator.Setup(m => m.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(IdentityResult.Success)
                .Verifiable();
            mediator.Setup(m => m.Send(It.IsAny<CreateTokenCommand>(), It.IsAny<CancellationToken>()))
                .Verifiable();

            IAuthService authService = new AuthService(mediator.Object, NullLoggerFactory.Instance);
            var registerModel = new RegisterModel() { Email = user.Email, Password = "123456", Name = user.UserName };
            await authService.Register(registerModel);

            mediator.VerifyAll();
        }

        [Fact]
        public async Task RegisterUserTestThrowsUserExistsException()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetUserByEmailQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(user)
                .Verifiable();

            IAuthService authService = new AuthService(mediator.Object, NullLoggerFactory.Instance);
            var registerModel = new RegisterModel() { Email = user.Email, Password = "123456", Name = user.UserName };
            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await authService.Register(registerModel));

            mediator.VerifyAll();
            Assert.Equal(AuthServiceStrings.UserExistsException, exception.Message);
        }

        [Fact]
        public async Task RegisterUserTestGetUserThrowsException()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetUserByEmailQuery>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception())
                .Verifiable();

            IAuthService authService = new AuthService(mediator.Object, NullLoggerFactory.Instance);
            var registerModel = new RegisterModel() { Email = user.Email, Password = "123456", Name = user.UserName };
            var exception = await Assert.ThrowsAsync<Exception>(async () => await authService.Register(registerModel));

            mediator.VerifyAll();
            Assert.Equal(AuthServiceStrings.RegisterException, exception.Message);
        }
        
        [Fact]
        public async Task RegisterUserTestCreateUserThrowsException()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetUserByEmailQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((User)null)
                .Verifiable();
            mediator.Setup(m => m.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception())
                .Verifiable();

            IAuthService authService = new AuthService(mediator.Object, NullLoggerFactory.Instance);
            var registerModel = new RegisterModel() { Email = user.Email, Password = "123456", Name = user.UserName };
            var exception = await Assert.ThrowsAsync<Exception>(async () => await authService.Register(registerModel));

            mediator.VerifyAll();
            Assert.Equal(AuthServiceStrings.RegisterException, exception.Message);
        }
        
        [Fact]
        public async Task RegisterUserTestCreateTokenThrowsException()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetUserByEmailQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((User)null)
                .Verifiable();
            mediator.Setup(m => m.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(IdentityResult.Success)
                .Verifiable();
            mediator.Setup(m => m.Send(It.IsAny<CreateTokenCommand>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception())
                .Verifiable();

            IAuthService authService = new AuthService(mediator.Object, NullLoggerFactory.Instance);
            var registerModel = new RegisterModel() { Email = user.Email, Password = "123456", Name = user.UserName };
            var exception = await Assert.ThrowsAsync<Exception>(async () => await authService.Register(registerModel));

            mediator.VerifyAll();
            Assert.Equal(AuthServiceStrings.RegisterException, exception.Message);
        }
        
        [Fact]
        public async Task RegisterUserFailedTest()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetUserByEmailQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((User)null)
                .Verifiable();
            mediator.Setup(m => m.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(IdentityResult.Failed())
                .Verifiable();

            IAuthService authService = new AuthService(mediator.Object, NullLoggerFactory.Instance);
            var registerModel = new RegisterModel() { Email = user.Email, Password = "123456", Name = user.UserName };
            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await authService.Register(registerModel));

            mediator.VerifyAll();
            Assert.Equal(AuthServiceStrings.RegisterFailedException, exception.Message);
        }

        [Fact]
        public async Task LogoutUserTest()
        {
            mediator.Setup(m => m.Send(It.IsAny<SignOutUserCommand>(), It.IsAny<CancellationToken>()))
                .Verifiable();

            var authService = new AuthService(mediator.Object, NullLoggerFactory.Instance);
            await authService.Logout();

            mediator.VerifyAll();
        }
        
        [Fact]
        public async Task LogoutUserTestThrowsException()
        {
            mediator.Setup(m => m.Send(It.IsAny<SignOutUserCommand>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception())
                .Verifiable();

            var authService = new AuthService(mediator.Object, NullLoggerFactory.Instance);
            var exception = await Assert.ThrowsAsync<Exception>(async () => await authService.Logout());

            mediator.VerifyAll();
            Assert.Equal(AuthServiceStrings.LogoutException, exception.Message);
        }
        
        [Fact]
        public async Task ChangePasswordTest()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetUserByEmailQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(user)
                .Verifiable();
            mediator.Setup(m => m.Send(It.IsAny<ChangePasswordCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(IdentityResult.Success)
                .Verifiable();

            var authService = new AuthService(mediator.Object, NullLoggerFactory.Instance);
            await authService.ChangePasswordAsync(new ChangePasswordModel()
            {
                Email = user.Email,
                NewPassword = "1234567",
                OldPassword = "123456"
            }, CancellationToken.None);

            mediator.VerifyAll();
        }

        [Fact]
        public async Task ChangePasswordTestGetUserThrowsException()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetUserByEmailQuery>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception())
                .Verifiable();

            var authService = new AuthService(mediator.Object, NullLoggerFactory.Instance);
            var exception = await Assert.ThrowsAsync<Exception>(async()=> await authService.ChangePasswordAsync(new ChangePasswordModel()
            {
                Email = user.Email,
                NewPassword = "1234567",
                OldPassword = "123456"
            }, CancellationToken.None));

            mediator.VerifyAll();
            Assert.Equal(AuthServiceStrings.ChangePasswordException, exception.Message);
        }
        
        [Fact]
        public async Task ChangePasswordTestThrowsNotFoundException()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetUserByEmailQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((User) null)
                .Verifiable();

            var authService = new AuthService(mediator.Object, NullLoggerFactory.Instance);
            var exception = await Assert.ThrowsAsync<ArgumentException>(async()=> await authService.ChangePasswordAsync(new ChangePasswordModel()
            {
                Email = user.Email,
                NewPassword = "1234567",
                OldPassword = "123456"
            }, CancellationToken.None));

            mediator.VerifyAll();
            Assert.Equal(AuthServiceStrings.ChangePasswordExceptionUserNotFound, exception.Message);
        }
        
        [Fact]
        public async Task ChangePasswordTestThrowsException()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetUserByEmailQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(user)
                .Verifiable();
            mediator.Setup(m => m.Send(It.IsAny<ChangePasswordCommand>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception())
                .Verifiable();

            var authService = new AuthService(mediator.Object, NullLoggerFactory.Instance);
            var exception = await Assert.ThrowsAsync<Exception>(async()=> await authService.ChangePasswordAsync(new ChangePasswordModel()
            {
                Email = user.Email,
                NewPassword = "1234567",
                OldPassword = "123456"
            }, CancellationToken.None));

            mediator.VerifyAll();
            Assert.Equal(AuthServiceStrings.ChangePasswordException, exception.Message);
        }
        
    }
}
