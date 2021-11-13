using FluentAssertions;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging.Abstractions;
using Domain.Entities.Auth;
using Application.Queries.Users;
using Application.Interfaces.Services;
using Infrastructure.Services;
using Application.Commands.Users;
using Application.DTO;
using Application.Queries.Auth;
using AutoMapper;
using Domain.Entities.Surveys;
using Domain.Enums;
using Domain.Models;
using Domain.Service;

namespace Application.UnitTests
{
    public class UserServiceTests
    {
        private readonly Mock<IMediator> mediator = new();
        private readonly Mock<IMapper> mapper = new();
        private readonly IEnumerable<User> users = TestData.GetTestUsers();
        private readonly List<UserDTO> userDtos = TestData.GetTestUserDtos();
        private readonly User user = TestData.GetTestUsers()[0];
        private readonly UserDTO userDto = TestData.GetTestUserDtos()[0];

        [Fact]
        public async Task GetUsersTest()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetUsersQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(users)
                .Verifiable();
            mapper
                .Setup(m => m.Map<IEnumerable<User>, IEnumerable<UserDTO>>(It.IsAny<IEnumerable<User>>()))
                .Returns(userDtos);

            IUsersService userService = new UsersService(mediator.Object, NullLoggerFactory.Instance, mapper.Object);

            var actual = await userService.GetUsersAsync(null);

            mediator.VerifyAll();
            var actualSurveys = actual.ToList();

            actualSurveys.Should().BeEquivalentTo(users, c => c.IgnoringCyclicReferences());
        }

        [Fact]
        public async Task GetUsersTestThrowsInnerException()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetUsersQuery>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception())
                .Verifiable();

            IUsersService userService = new UsersService(mediator.Object, NullLoggerFactory.Instance, mapper.Object);

            var exception = await Assert.ThrowsAsync<Exception>(async () => await userService.GetUsersAsync(null));

            Assert.Equal(UserServiceStrings.GetUsersException, exception.Message);

            mediator.VerifyAll();
        }

        [Fact]
        public async Task GetUsersByRoleTest()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetUsersInRoleQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(users)
                .Verifiable();
            mapper
                .Setup(m => m.Map<IEnumerable<UserDTO>>(It.IsAny<IEnumerable<User>>()))
                .Returns(userDtos);

            IUsersService userService = new UsersService(mediator.Object, NullLoggerFactory.Instance, mapper.Object);

            var actual = await userService.GetUsersAsync("admin");

            mediator.VerifyAll();
            var actualSurveys = actual.ToList();

            actualSurveys.Should().BeEquivalentTo(users, c => c.IgnoringCyclicReferences());
        }

        [Fact]
        public async Task GetUsersByRoleTestThrowsRoleException()
        {
            IUsersService userService = new UsersService(mediator.Object, NullLoggerFactory.Instance, mapper.Object);

            var exception =
                await Assert.ThrowsAsync<ArgumentException>(async () => await userService.GetUsersAsync("role"));

            Assert.Equal(UserServiceStrings.GetUsersRoleException, exception.Message);

            mediator.VerifyAll();
        }

        [Fact]
        public async Task GetUsersByRoleTestThrowsInnerException()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetUsersInRoleQuery>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception())
                .Verifiable();
            mapper
                .Setup(m => m.Map<IEnumerable<UserDTO>>(It.IsAny<IEnumerable<User>>()))
                .Returns(userDtos);

            IUsersService userService = new UsersService(mediator.Object, NullLoggerFactory.Instance, mapper.Object);

            var exception =
                await Assert.ThrowsAsync<Exception>(async () => await userService.GetUsersAsync("client"));

            Assert.Equal(UserServiceStrings.GetUsersException, exception.Message);

            mediator.VerifyAll();
        }

        [Fact]
        public async Task GetUsersTestThrowsNullException()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetUsersQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((IEnumerable<User>) null)
                .Verifiable();

            IUsersService userService = new UsersService(mediator.Object, NullLoggerFactory.Instance, null);

            var exception =
                await Assert.ThrowsAsync<ArgumentException>(async () => await userService.GetUsersAsync(null));

            mediator.VerifyAll();

            Assert.Equal(UserServiceStrings.GetUsersNullException, exception.Message);
        }

        [Fact]
        public async Task GetUserByIdTest()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetUserByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(user)
                .Verifiable();
            mediator.Setup(m => m.Send(It.IsAny<GetRolesByUserQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(userDto.Roles)
                .Verifiable();
            mapper
                .Setup(m => m.Map<UserDTO>(It.IsAny<User>()))
                .Returns(userDto);

            IUsersService surveyService = new UsersService(mediator.Object, NullLoggerFactory.Instance, mapper.Object);

            var actual = await surveyService.GetUserByIdAsync(userDto.Id);

            mediator.VerifyAll();

            actual.Should().BeEquivalentTo(userDto, c => c.IgnoringCyclicReferences());
        }

        [Fact]
        public async Task GetUserByIdTestThrowsIdException()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetUserByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((User) null)
                .Verifiable();

            IUsersService userService = new UsersService(mediator.Object, NullLoggerFactory.Instance, null);

            var exception =
                await Assert.ThrowsAsync<ArgumentException>(async () => await userService.GetUserByIdAsync(new Guid()));

            mediator.VerifyAll();

            Assert.Equal(UserServiceStrings.GetUserIdException, exception.Message);
        }

        [Fact]
        public async Task GetUserByIdTestThrowsInnerException()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetUserByIdQuery>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception())
                .Verifiable();

            IUsersService userService = new UsersService(mediator.Object, NullLoggerFactory.Instance, null);

            var exception =
                await Assert.ThrowsAsync<Exception>(async () => await userService.GetUserByIdAsync(new Guid()));

            mediator.VerifyAll();

            Assert.Equal(UserServiceStrings.GetUserException, exception.Message);
        }

        [Fact]
        public async Task GetUserByIdTestThrowsRolesException()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetUserByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(user)
                .Verifiable();
            mediator.Setup(m => m.Send(It.IsAny<GetRolesByUserQuery>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception())
                .Verifiable();
            mapper
                .Setup(m => m.Map<UserDTO>(It.IsAny<User>()))
                .Returns(userDto);

            IUsersService surveyService = new UsersService(mediator.Object, NullLoggerFactory.Instance, mapper.Object);

            var exception =
                await Assert.ThrowsAsync<Exception>(
                    async () => await surveyService.GetUserByIdAsync(userDto.Id));

            mediator.VerifyAll();

            Assert.Equal(UserServiceStrings.GetRolesException, exception.Message);
        }

        [Fact]
        public async Task GetUserByEmailTest()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetUserByEmailQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(user)
                .Verifiable();
            mediator.Setup(m => m.Send(It.IsAny<GetRolesByUserQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(userDto.Roles)
                .Verifiable();
            mapper
                .Setup(m => m.Map<UserDTO>(It.IsAny<User>()))
                .Returns(userDto);

            IUsersService surveyService = new UsersService(mediator.Object, NullLoggerFactory.Instance, mapper.Object);

            var actual = await surveyService.GetUserByEmailAsync(userDto.Email);

            mediator.VerifyAll();

            actual.Should().BeEquivalentTo(userDto, c => c.IgnoringCyclicReferences());
        }

        [Fact]
        public async Task GetUserByEmailTestThrowsInnerException()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetUserByEmailQuery>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception())
                .Verifiable();

            IUsersService surveyService = new UsersService(mediator.Object, NullLoggerFactory.Instance, mapper.Object);

            var exception = await Assert.ThrowsAsync<Exception>
                (async () => await surveyService.GetUserByEmailAsync(userDto.Email));

            mediator.VerifyAll();

            Assert.Equal(UserServiceStrings.GetUserException, exception.Message);
        }

        [Fact]
        public async Task GetUserByEmailTestThrowsEmailException()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetUserByEmailQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((User) null)
                .Verifiable();
            mapper
                .Setup(m => m.Map<UserDTO>(It.IsAny<User>()))
                .Returns(userDto);

            IUsersService surveyService = new UsersService(mediator.Object, NullLoggerFactory.Instance, mapper.Object);

            var exception = await Assert.ThrowsAsync<ArgumentException>
                (async () => await surveyService.GetUserByEmailAsync(userDto.Email));

            mediator.VerifyAll();

            Assert.Equal(UserServiceStrings.GetUserEmailException, exception.Message);
        }

        [Fact]
        public async Task GetUserByEmailTestThrowsMapperException()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetUserByEmailQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(user)
                .Verifiable();

            mapper.Setup(m => m.Map<UserDTO>(It.IsAny<User>()))
                .Throws(new Exception());

            IUsersService surveyService = new UsersService(mediator.Object, NullLoggerFactory.Instance, mapper.Object);

            var exception = await Assert.ThrowsAsync<Exception>
                (async () => await surveyService.GetUserByEmailAsync(userDto.Email));

            mediator.VerifyAll();

            Assert.Equal(UserServiceStrings.GetUserException, exception.Message);
        }

        [Fact]
        public async Task GetUserByEmailTestThrowsRolesException()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetUserByEmailQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(user)
                .Verifiable();
            mediator.Setup(m => m.Send(It.IsAny<GetRolesByUserQuery>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception())
                .Verifiable();
            mapper
                .Setup(m => m.Map<UserDTO>(It.IsAny<User>()))
                .Returns(userDto);

            IUsersService surveyService = new UsersService(mediator.Object, NullLoggerFactory.Instance, mapper.Object);

            var exception = await Assert.ThrowsAsync<Exception>
                (async () => await surveyService.GetUserByEmailAsync(userDto.Email));

            mediator.VerifyAll();

            Assert.Equal(UserServiceStrings.GetRolesException, exception.Message);
        }

        [Fact]
        public async Task DeleteUserTest()
        {
            mediator.Setup(m => m.Send(It.IsAny<DeleteUserCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(IdentityResult.Success)
                .Verifiable();

            IUsersService surveyService = new UsersService(mediator.Object, NullLoggerFactory.Instance, null);
            var actual = await surveyService.DeleteUserAsync(user.Id);

            Assert.True(actual);

            mediator.VerifyAll();
        }

        [Fact]
        public async Task DeleteUserTestThrowsInnerException()
        {
            mediator.Setup(m => m.Send(It.IsAny<DeleteUserCommand>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception())
                .Verifiable();

            IUsersService surveyService = new UsersService(mediator.Object, NullLoggerFactory.Instance, null);

            var exception = await Assert.ThrowsAsync<Exception>
                (async () => await surveyService.DeleteUserAsync(user.Id));

            mediator.VerifyAll();

            Assert.Equal(UserServiceStrings.DeleteUserException, exception.Message);
        }

        [Fact]
        public async Task DeleteUserFailedTest()
        {
            mediator.Setup(m => m.Send(It.IsAny<DeleteUserCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(IdentityResult.Failed())
                .Verifiable();

            IUsersService surveyService = new UsersService(mediator.Object, NullLoggerFactory.Instance, null);

            var actual = await surveyService.DeleteUserAsync(user.Id);

            Assert.False(actual);

            mediator.VerifyAll();
        }

        [Fact]
        public async Task DisableUserTest()
        {
            mediator.Setup(m => m.Send(It.IsAny<DisableUserCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true)
                .Verifiable();

            IUsersService surveyService = new UsersService(mediator.Object, NullLoggerFactory.Instance, null);
            var actual = await surveyService.DisableUserAsync(new DisableUserModel()
            {
                UserId = user.Id,
                DisableEnd = DateTime.Now.AddYears(1)
            });

            Assert.True(actual);

            mediator.VerifyAll();
        }

        [Fact]
        public async Task DisableUserTestThrowsInnerException()
        {
            mediator.Setup(m => m.Send(It.IsAny<DisableUserCommand>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception())
                .Verifiable();
            IUsersService surveyService = new UsersService(mediator.Object, NullLoggerFactory.Instance, null);
            var exception = await Assert.ThrowsAsync<Exception>
            (async () => await surveyService.DisableUserAsync(new DisableUserModel()
            {
                UserId = user.Id,
                DisableEnd = DateTime.Now.AddYears(1)
            }));

            Assert.Equal(UserServiceStrings.DisableUserException, exception.Message);

            mediator.VerifyAll();
        }

        [Fact]
        public async Task DisableUserTestThrowsTimeException()
        {
            IUsersService surveyService = new UsersService(mediator.Object, NullLoggerFactory.Instance, null);
            var exception = await Assert.ThrowsAsync<ArgumentException>
            (async () => await surveyService.DisableUserAsync(new DisableUserModel()
            {
                UserId = user.Id,
                DisableEnd = DateTime.Now.AddYears(-1)
            }));

            Assert.Equal(UserServiceStrings.DisableUserTimeException, exception.Message);

            mediator.VerifyAll();
        }

        [Fact]
        public async Task DisableUserFailedTest()
        {
            mediator.Setup(m => m.Send(It.IsAny<DisableUserCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false)
                .Verifiable();
            IUsersService surveyService = new UsersService(mediator.Object, NullLoggerFactory.Instance, null);
            var actual = await surveyService.DisableUserAsync(new DisableUserModel()
            {
                UserId = user.Id,
                DisableEnd = DateTime.Now.AddYears(1)
            });

            Assert.False(actual);

            mediator.VerifyAll();
        }

        [Fact]
        public async Task EnableUserTest()
        {
            mediator.Setup(m => m.Send(It.IsAny<EnableUserCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true)
                .Verifiable();

            IUsersService surveyService = new UsersService(mediator.Object, NullLoggerFactory.Instance, null);
            var actual = await surveyService.EnableUserAsync(user.Id);

            Assert.True(actual);

            mediator.VerifyAll();
        }

        [Fact]
        public async Task EnableUserFailedTest()
        {
            mediator.Setup(m => m.Send(It.IsAny<EnableUserCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false)
                .Verifiable();

            IUsersService surveyService = new UsersService(mediator.Object, NullLoggerFactory.Instance, null);
            var actual = await surveyService.EnableUserAsync(user.Id);

            Assert.False(actual);

            mediator.VerifyAll();
        }

        [Fact]
        public async Task EnableUserTestThrowsInnerException()
        {
            mediator.Setup(m => m.Send(It.IsAny<EnableUserCommand>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception())
                .Verifiable();

            IUsersService surveyService = new UsersService(mediator.Object, NullLoggerFactory.Instance, null);
            var exception = await Assert.ThrowsAsync<Exception>
                (async () => await surveyService.EnableUserAsync(user.Id));

            Assert.Equal(UserServiceStrings.EnableUserException, exception.Message);

            mediator.VerifyAll();
        }

        [Fact]
        public async Task RemoveFromRoleTest()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetUserByEmailQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(user)
                .Verifiable();
            mediator.Setup(m => m.Send(It.IsAny<GetRolesByUserQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new[] {"admin"})
                .Verifiable();
            mediator.Setup(m => m.Send(It.IsAny<RemoveUserFromRoleCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true)
                .Verifiable();

            IUsersService surveyService = new UsersService(mediator.Object, NullLoggerFactory.Instance, null);
            var actual = await surveyService.RemoveUserFromRoleAsync(new RemoveUserFromRoleModel()
            {
                Role = "client",
                UserId = Guid.NewGuid()
            }, user.Email, CancellationToken.None);

            Assert.True(actual);

            mediator.VerifyAll();
        }

        [Fact]
        public async Task RemoveFromRoleTestThrowsRoleException()
        {
            IUsersService surveyService = new UsersService(mediator.Object, NullLoggerFactory.Instance, null);
            var exception = await Assert.ThrowsAsync<Exception>(async () => await surveyService.RemoveUserFromRoleAsync(
                new RemoveUserFromRoleModel()
                {
                    Role = "role",
                    UserId = Guid.NewGuid()
                }, user.Email, CancellationToken.None));

            Assert.Equal(UserServiceStrings.RemoveUserFromRoleExceptionRole, exception.Message);

            mediator.VerifyAll();
        }

        [Fact]
        public async Task RemoveFromRoleTestGetUserThrowsException()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetUserByEmailQuery>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception())
                .Verifiable();

            IUsersService surveyService = new UsersService(mediator.Object, NullLoggerFactory.Instance, null);
            var exception = await Assert.ThrowsAsync<Exception>(async () => await surveyService.RemoveUserFromRoleAsync(
                new RemoveUserFromRoleModel
                {
                    Role = "client",
                    UserId = Guid.NewGuid()
                }, user.Email, CancellationToken.None));

            Assert.Equal(UserServiceStrings.GetUserException, exception.Message);

            mediator.VerifyAll();
        }

        [Fact]
        public async Task RemoveFromRoleTestGetRolesThrowsException()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetUserByEmailQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(user)
                .Verifiable();
            mediator.Setup(m => m.Send(It.IsAny<GetRolesByUserQuery>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception())
                .Verifiable();

            IUsersService surveyService = new UsersService(mediator.Object, NullLoggerFactory.Instance, null);
            var exception = await Assert.ThrowsAsync<Exception>(async () => await surveyService.RemoveUserFromRoleAsync(
                new RemoveUserFromRoleModel
                {
                    Role = "client",
                    UserId = Guid.NewGuid()
                }, user.Email, CancellationToken.None));

            Assert.Equal(UserServiceStrings.GetRolesException, exception.Message);

            mediator.VerifyAll();
        }

        [Fact]
        public async Task RemoveFromRoleTestThrowsCurrentRoleException()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetUserByEmailQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(user)
                .Verifiable();
            mediator.Setup(m => m.Send(It.IsAny<GetRolesByUserQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new[] {"admin"})
                .Verifiable();

            IUsersService surveyService = new UsersService(mediator.Object, NullLoggerFactory.Instance, null);
            var exception = await Assert.ThrowsAsync<Exception>(async () => await surveyService.RemoveUserFromRoleAsync(
                new RemoveUserFromRoleModel
                {
                    Role = "admin",
                    UserId = Guid.NewGuid()
                }, user.Email, CancellationToken.None));

            Assert.Equal(UserServiceStrings.RemoveFromRoleCurrentRoleException, exception.Message);

            mediator.VerifyAll();
        }

        [Fact]
        public async Task RemoveFromRoleFailedTest()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetUserByEmailQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(user)
                .Verifiable();
            mediator.Setup(m => m.Send(It.IsAny<GetRolesByUserQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new[] {"admin"})
                .Verifiable();
            mediator.Setup(m => m.Send(It.IsAny<RemoveUserFromRoleCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false)
                .Verifiable();

            IUsersService surveyService = new UsersService(mediator.Object, NullLoggerFactory.Instance, null);
            var actual = await surveyService.RemoveUserFromRoleAsync(new RemoveUserFromRoleModel()
            {
                Role = "client",
                UserId = Guid.NewGuid()
            }, user.Email, CancellationToken.None);

            Assert.False(actual);

            mediator.VerifyAll();
        }
    }
}