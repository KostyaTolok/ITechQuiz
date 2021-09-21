using FluentAssertions;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using System;
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

namespace Application.UnitTests
{
    public class UserServiceTests
    {
        private readonly Mock<IMediator> mediator = new();
        private readonly Mock<ILoggerFactory> logger = new();
        private readonly IEnumerable<User> users = TestData.GetTestUsers();
        private readonly User user = TestData.GetTestUsers()[0];

        [Fact]
        public async Task GetUsersTest()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetUsersQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(users)
                .Verifiable();

            IUserService userService = new UserService(mediator.Object, logger.Object);

            var actual = await userService.GetUsersAsync();

            mediator.Verify();
            var actualSurveys = actual.ToList();

            actualSurveys.Should().BeEquivalentTo(users, config: c => c.IgnoringCyclicReferences());
        }

        [Fact]
        public async Task GetUsersTestThrowsArgumentException()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetUsersQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((IEnumerable<User>)null)
                .Verifiable();

            IUserService userService = new UserService(mediator.Object, NullLoggerFactory.Instance);

            ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(async () => await userService.GetUsersAsync());

            mediator.Verify();

            Assert.Equal("Failed to get users", exception.Message);
        }

        [Fact]
        public async Task GetUserByIdTest()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetUserByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(user)
                .Verifiable();

            IUserService surveyService = new UserService(mediator.Object, logger.Object);

            var actual = await surveyService.GetUserAsync(user.Id);

            mediator.Verify();

            actual.Should().BeEquivalentTo(user, config: c => c.IgnoringCyclicReferences());
        }

        [Fact]
        public async Task GetUserByIdTestThrowsArgumentException()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetUserByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((User)null)
                .Verifiable();

            IUserService userService = new UserService(mediator.Object, NullLoggerFactory.Instance);

            ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(async () => await userService.GetUserAsync(new Guid()));

            mediator.Verify();

            Assert.Equal("Failed to get user. Wrong id", exception.Message);
        }

        [Fact]
        public async Task DeleteUserTest()
        {
            mediator.Setup(m => m.Send(It.IsAny<DeleteUserCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(IdentityResult.Success)
                .Verifiable();

            IUserService surveyService = new UserService(mediator.Object, logger.Object);
            await surveyService.DeleteUserAsync(user.Id);

            mediator.Verify();
        }

    }
}
