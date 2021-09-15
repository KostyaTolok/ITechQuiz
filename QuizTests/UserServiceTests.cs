using FluentAssertions;
using ITechQuiz.Models;
using ITechQuiz.Services.SurveyServices;
using ITechQuiz.Services.UserServices;
using ITechQuiz.Services.UserServices.Queries;
using ITechQuiz.Services.UserServices.Commands;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Identity;

namespace QuizTests
{
    public class UserServiceTests
    {
        private readonly Mock<IMediator> mediator = new();
        private readonly Mock<ILogger<SurveyService>> logger = new();

        [Fact]
        public async Task GetUsersTest()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetUsersQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(GetTestUsers())
                .Verifiable();

            IUserService userService = new UserService(mediator.Object, logger.Object);

            var actual = await userService.GetUsersAsync();
            var expected = GetTestUsers();

            mediator.Verify();
            var actualSurveys = actual.ToList();

            actualSurveys.Should().BeEquivalentTo(expected, config: c => c.IgnoringCyclicReferences());
        }

        [Fact]
        public async Task GetUsersTestThrowsArgumentException()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetUsersQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((IEnumerable<User>)null)
                .Verifiable();

            IUserService userService = new UserService(mediator.Object, logger.Object);

            ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(async () => await userService.GetUsersAsync());

            mediator.Verify();

            Assert.Equal("Failed to get users", exception.Message);
        }

        [Fact]
        public async Task GetUserByIdTest()
        {
            var expected = GetTestUsers()[0];
            mediator.Setup(m => m.Send(It.IsAny<GetUserByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expected)
                .Verifiable();

            IUserService surveyService = new UserService(mediator.Object, logger.Object);

            var actual = await surveyService.GetUserAsync(expected.Id);

            mediator.Verify();

            actual.Should().BeEquivalentTo(expected, config: c => c.IgnoringCyclicReferences());
        }

        [Fact]
        public async Task GetUserByIdTestThrowsArgumentException()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetUserByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((User)null)
                .Verifiable();

            IUserService userService = new UserService(mediator.Object, logger.Object);

            ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(async () => await userService.GetUserAsync(new Guid()));

            mediator.Verify();

            Assert.Equal("Failed to get user. Wrong id", exception.Message);
        }

        [Fact]
        public async Task LoginUserTest()
        {
            var expected = GetTestUsers()[0];
            mediator.Setup(m => m.Send(It.IsAny<PasswordSignInUserCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(SignInResult.Success)
                .Verifiable();

            IUserService surveyService = new UserService(mediator.Object, logger.Object);
            var loginModel = new LoginModel() { Email = expected.Email, Password = "123456", RememberMe = true };
            await surveyService.Login(loginModel);

            mediator.Verify();
        }

        [Fact]
        public async Task LoginUserTestThrowsArgumentException()
        {
            mediator.Setup(m => m.Send(It.IsAny<PasswordSignInUserCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(SignInResult.Failed)
                .Verifiable();

            IUserService surveyService = new UserService(mediator.Object, logger.Object);
            var loginModel = new LoginModel() { Email = "", Password = "", RememberMe = true };

            ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(async () => await surveyService.Login(loginModel));

            mediator.Verify();

            Assert.Equal("Wrong login or password", exception.Message);
        }

        [Fact]
        public async Task DeleteUserTest()
        {
            var toDelete = GetTestUsers()[0];
            mediator.Setup(m => m.Send(It.IsAny<DeleteUserCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(IdentityResult.Success)
                .Verifiable();

            IUserService surveyService = new UserService(mediator.Object, logger.Object);
            await surveyService.DeleteUserAsync(toDelete.Id);

            mediator.Verify();
        }

        private static List<User> GetTestUsers()
        {
            var survey = new Survey()
            {
                Id = new Guid("827f753c-7544-463b-9fa4-d9c5c051cf17"),
                Name = "MySurv",
                CreatedDate = DateTime.Now.Date,
                Title = "Это опрос",
                Subtitle = "Спасибо за прохождение опроса",
                Type = SurveyType.ForStatistics,
                UserId = new Guid("78f33f64-29cc-4b47-82ab-bffd90c177a2"),
                Questions = new List<Question>()
                {
                    new Question()
                    {
                        Id = new Guid("bc6d54ae-5da2-477f-a02f-fbff4c73a638"),
                        Title = "Первый вопрос",
                        Multiple = false,
                        MaxSelected = 1,
                        Required = false,
                        SurveyName = "MySurv",
                        Options = new List<Option>()
                        {
                            new Option()
                            {
                                Id = new Guid("bc6d54ae-5da2-477f-a02f-fbff4c73a438"),
                                Title = "Это вариант",
                                IsCorrect = true,
                                Subtitle = "Блаблабла"
                            }
                        }
                    }
                }
            };

            var user = new User()
            {
                Email = "user@mail.com",
                UserName = "User",
                ConcurrencyStamp = string.Empty,
                Surveys = new List<Survey>() { survey }
            };

            var users = new List<User> { user };
            return users;
        }
    }
}
