using System;
using System.Collections;
using Xunit;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using System.Threading;
using MediatR;
using Microsoft.Extensions.Logging.Abstractions;
using Domain.Entities.Surveys;
using Application.Queries.Surveys;
using Application.Interfaces.Services;
using Infrastructure.Services;
using Application.Commands.Surveys;
using Application.DTO;
using Application.Queries.Auth;
using AutoMapper;
using Domain.Entities.Auth;
using Domain.Service;

namespace Application.UnitTests
{
    public class SurveyServiceTests
    {
        private readonly Mock<IMediator> mediator = new();
        private readonly Mock<IMapper> mapper = new();
        private readonly IEnumerable<Survey> surveys = TestData.GetTestSurveys();
        private readonly IEnumerable<SurveyDTO> surveyDtos = TestData.GetTestSurveyDtos();
        private readonly Survey survey = TestData.GetTestSurveys()[0];
        private readonly SurveyDTO surveyDto = TestData.GetTestSurveyDtos()[0];
        private readonly User user = TestData.GetTestUsers()[0];

        [Fact]
        public async Task GetSurveysTest()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetSurveysQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(surveys)
                .Verifiable();
            mapper.Setup(m => m.Map<IEnumerable<SurveyDTO>>(It.IsAny<IEnumerable<Survey>>()))
                .Returns(surveyDtos);
            ISurveysService surveyService =
                new SurveysService(mediator.Object, NullLoggerFactory.Instance, mapper.Object);

            var actual = await surveyService.GetSurveysAsync(null, TODO, null, new List<Guid>(), CancellationToken.None);

            mediator.VerifyAll();
            var actualSurveys = actual.ToList();

            actualSurveys.Should().BeEquivalentTo(surveyDtos, c => c.IgnoringCyclicReferences());
        }

        [Fact]
        public async Task GetSurveysTestTrowsArgumentException()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetSurveysQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((IEnumerable<Survey>) null)
                .Verifiable();

            ISurveysService surveyService = new SurveysService(mediator.Object, NullLoggerFactory.Instance, null);

            var exception = await Assert.ThrowsAsync<ArgumentException>
                (async () => await surveyService.GetSurveysAsync(null, TODO, null, new List<Guid>(), default));

            mediator.VerifyAll();

            Assert.Equal(SurveyServiceStrings.GetSurveysNullException, exception.Message);
        }

        [Fact]
        public async Task GetSurveysTestTrowsInternalException()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetSurveysQuery>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception())
                .Verifiable();

            ISurveysService surveyService = new SurveysService(mediator.Object, NullLoggerFactory.Instance, null);

            var exception = await Assert.ThrowsAsync<Exception>
                (async () => await surveyService.GetSurveysAsync(null, TODO, null, new List<Guid>(), default));

            mediator.VerifyAll();

            Assert.Equal(SurveyServiceStrings.GetSurveysException, exception.Message);
        }

        [Fact]
        public async Task GetSurveyTest()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetSurveyByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(survey)
                .Verifiable();
            mapper.Setup(m => m.Map<SurveyDTO>(It.IsAny<Survey>()))
                .Returns(surveyDto);
            ISurveysService surveyService =
                new SurveysService(mediator.Object, NullLoggerFactory.Instance, mapper.Object);

            var actual = await surveyService.GetSurveyAsync(survey.Id, default);

            mediator.VerifyAll();

            actual.Should().BeEquivalentTo(surveyDto, c => c.IgnoringCyclicReferences());
        }

        [Fact]
        public async Task GetSurveyTestThrowsArgumentException()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetSurveyByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Survey) null)
                .Verifiable();

            ISurveysService surveyService = new SurveysService(mediator.Object, NullLoggerFactory.Instance, null);

            var exception =
                await Assert.ThrowsAsync<ArgumentException>(async () =>
                    await surveyService.GetSurveyAsync(new Guid(), default));

            mediator.VerifyAll();

            Assert.Equal(SurveyServiceStrings.GetSurveyIdException, exception.Message);
        }

        [Fact]
        public async Task GetSurveyTestThrowsInternalException()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetSurveyByIdQuery>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception())
                .Verifiable();

            ISurveysService surveyService = new SurveysService(mediator.Object, NullLoggerFactory.Instance, null);

            var exception =
                await Assert.ThrowsAsync<Exception>(async () =>
                    await surveyService.GetSurveyAsync(new Guid(), default));

            mediator.VerifyAll();

            Assert.Equal(SurveyServiceStrings.GetSurveyException, exception.Message);
        }

        [Fact]
        public async Task AddSurveyTest()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetUserByEmailQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(user).Verifiable();
            mediator.Setup(m => m.Send(It.IsAny<AddSurveyCommand>(), It.IsAny<CancellationToken>()))
                .Verifiable();
            mapper.Setup(m => m.Map<Survey>(surveyDto)).Returns(survey);

            ISurveysService surveyService =
                new SurveysService(mediator.Object, NullLoggerFactory.Instance, mapper.Object);

            await surveyService.AddSurveyAsync(surveyDto, user.Id, default);

            mediator.VerifyAll();
        }

        [Fact]
        public async Task AddSurveyTestThrowsTitleException()
        {
            survey.Title = "";
            mediator.Setup(m => m.Send(It.IsAny<GetUserByEmailQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(user).Verifiable();
            mediator.Setup(m => m.Send(It.IsAny<AddSurveyCommand>(), It.IsAny<CancellationToken>()))
                .Verifiable();
            mapper.Setup(m => m.Map<Survey>(surveyDto)).Returns(survey);

            ISurveysService surveyService =
                new SurveysService(mediator.Object, NullLoggerFactory.Instance, mapper.Object);

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () =>
                await surveyService.AddSurveyAsync(surveyDto, user.Id, default));

            Assert.Equal(SurveyServiceStrings.AddSurveyTitleException, exception.Message);
        }

        [Fact]
        public async Task AddSurveyTestThrowsQuestionTitleException()
        {
            survey.Questions.First().Title = "";
            mediator.Setup(m => m.Send(It.IsAny<GetUserByEmailQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(user).Verifiable();
            mediator.Setup(m => m.Send(It.IsAny<AddSurveyCommand>(), It.IsAny<CancellationToken>()))
                .Verifiable();
            mapper.Setup(m => m.Map<Survey>(surveyDto)).Returns(survey);

            ISurveysService surveyService =
                new SurveysService(mediator.Object, NullLoggerFactory.Instance, mapper.Object);

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () =>
                await surveyService.AddSurveyAsync(surveyDto, user.Id, default));

            Assert.Equal(SurveyServiceStrings.AddSurveyQuestionTitleException, exception.Message);
        }

        [Fact]
        public async Task AddSurveyTestThrowsOptionTitleException()
        {
            survey.Questions.First().Options.First().Title = "";
            mediator.Setup(m => m.Send(It.IsAny<GetUserByEmailQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(user).Verifiable();
            mediator.Setup(m => m.Send(It.IsAny<AddSurveyCommand>(), It.IsAny<CancellationToken>()))
                .Verifiable();
            mapper.Setup(m => m.Map<Survey>(surveyDto)).Returns(survey);

            ISurveysService surveyService =
                new SurveysService(mediator.Object, NullLoggerFactory.Instance, mapper.Object);

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () =>
                await surveyService.AddSurveyAsync(surveyDto, user.Id, default));

            Assert.Equal(SurveyServiceStrings.AddSurveyOptionTitleException, exception.Message);
        }

        [Fact]
        public async Task AddSurveyTestGetUserThrowsInternalException()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetUserByEmailQuery>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception()).Verifiable();
            mapper.Setup(m => m.Map<Survey>(surveyDto)).Returns(survey);

            ISurveysService surveyService =
                new SurveysService(mediator.Object, NullLoggerFactory.Instance, mapper.Object);

            var exception = await Assert.ThrowsAsync<Exception>(async () =>
                await surveyService.AddSurveyAsync(surveyDto, user.Id, default));

            Assert.Equal(UserServiceStrings.GetUserException, exception.Message);
        }

        [Fact]
        public async Task AddSurveyTestMapperThrowsInternalException()
        {
            mapper.Setup(m => m.Map<Survey>(surveyDto)).Throws(new Exception());

            ISurveysService surveyService =
                new SurveysService(mediator.Object, NullLoggerFactory.Instance, mapper.Object);

            var exception = await Assert.ThrowsAsync<Exception>(async () =>
                await surveyService.AddSurveyAsync(surveyDto, user.Id, default));

            Assert.Equal(SurveyServiceStrings.AddSurveyException, exception.Message);
        }

        [Fact]
        public async Task AddSurveyTestThrowsInternalException()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetUserByEmailQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(user).Verifiable();
            mediator.Setup(m => m.Send(It.IsAny<AddSurveyCommand>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception())
                .Verifiable();
            mapper.Setup(m => m.Map<Survey>(surveyDto)).Returns(survey);

            ISurveysService surveyService =
                new SurveysService(mediator.Object, NullLoggerFactory.Instance, mapper.Object);

            var exception = await Assert.ThrowsAsync<Exception>(async () =>
                await surveyService.AddSurveyAsync(surveyDto, user.Id, default));

            Assert.Equal(SurveyServiceStrings.AddSurveyException, exception.Message);
        }

        [Fact]
        public async Task AddSurveyTestThrowsExceptionNull()
        {
            mapper.Setup(m => m.Map<Survey>(surveyDto)).Returns(survey);

            ISurveysService surveyService =
                new SurveysService(mediator.Object, NullLoggerFactory.Instance, mapper.Object);

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () =>
                await surveyService.AddSurveyAsync(null, user.Id, default));

            Assert.Equal(SurveyServiceStrings.AddSurveyNullException, exception.Message);
        }

        [Fact]
        public async Task DeleteSurveyTest()
        {
            mediator.Setup(m => m.Send(It.IsAny<DeleteSurveyCommand>(), It.IsAny<CancellationToken>())).Verifiable();

            ISurveysService surveyService =
                new SurveysService(mediator.Object, NullLoggerFactory.Instance, mapper.Object);

            await surveyService.DeleteSurveyAsync(survey.Id, default);

            mediator.VerifyAll();
        }

        [Fact]
        public async Task DeleteSurveyTestThrowsArgumentException()
        {
            survey.Id = new Guid();

            mediator.Setup(m => m.Send(It.IsAny<DeleteSurveyCommand>(), It.IsAny<CancellationToken>()));

            ISurveysService surveyService =
                new SurveysService(mediator.Object, NullLoggerFactory.Instance, mapper.Object);

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () =>
                await surveyService.DeleteSurveyAsync(survey.Id, default));

            Assert.Equal(SurveyServiceStrings.DeleteSurveyIdException, exception.Message);
        }

        [Fact]
        public async Task DeleteSurveyTestThrowsInternalException()
        {
            mediator.Setup(m => m.Send(It.IsAny<DeleteSurveyCommand>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());

            ISurveysService surveyService =
                new SurveysService(mediator.Object, NullLoggerFactory.Instance, mapper.Object);

            var exception =
                await Assert.ThrowsAsync<Exception>(async () =>
                    await surveyService.DeleteSurveyAsync(survey.Id, default));

            Assert.Equal(SurveyServiceStrings.DeleteSurveyException, exception.Message);
        }

        [Fact]
        public async Task UpdateSurveyTest()
        {
            mediator.Setup(m => m.Send(It.IsAny<UpdateSurveyCommand>(), It.IsAny<CancellationToken>())).Verifiable();
            mapper.Setup(m => m.Map<Survey>(surveyDto)).Returns(survey);
            ISurveysService surveyService =
                new SurveysService(mediator.Object, NullLoggerFactory.Instance, mapper.Object);

            await surveyService.UpdateSurveyAsync(surveyDto, CancellationToken.None);

            mediator.VerifyAll();
        }

        [Fact]
        public async Task UpdateSurveyTestThrowsTitleException()
        {
            survey.Title = "";

            mediator.Setup(m => m.Send(It.IsAny<UpdateSurveyCommand>(), It.IsAny<CancellationToken>()));
            mapper.Setup(m => m.Map<Survey>(surveyDto)).Returns(survey);

            ISurveysService surveyService =
                new SurveysService(mediator.Object, NullLoggerFactory.Instance, mapper.Object);

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () =>
                await surveyService.UpdateSurveyAsync(surveyDto, default));

            Assert.Equal(SurveyServiceStrings.UpdateSurveyTitleException, exception.Message);
        }

        [Fact]
        public async Task UpdateSurveyTestThrowsQuestionTitleException()
        {
            survey.Questions.First().Title = "";

            mediator.Setup(m => m.Send(It.IsAny<UpdateSurveyCommand>(), It.IsAny<CancellationToken>()));
            mapper.Setup(m => m.Map<Survey>(surveyDto)).Returns(survey);

            ISurveysService surveyService =
                new SurveysService(mediator.Object, NullLoggerFactory.Instance, mapper.Object);

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () =>
                await surveyService.UpdateSurveyAsync(surveyDto, default));

            Assert.Equal(SurveyServiceStrings.UpdateSurveyQuestionTitleException, exception.Message);
        }

        [Fact]
        public async Task UpdateSurveyTestThrowsOptionTitleException()
        {
            survey.Questions.First().Options.First().Title = "";

            mediator.Setup(m => m.Send(It.IsAny<UpdateSurveyCommand>(), It.IsAny<CancellationToken>()));
            mapper.Setup(m => m.Map<Survey>(surveyDto)).Returns(survey);

            ISurveysService surveyService =
                new SurveysService(mediator.Object, NullLoggerFactory.Instance, mapper.Object);

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () =>
                await surveyService.UpdateSurveyAsync(surveyDto, default));

            Assert.Equal(SurveyServiceStrings.UpdateSurveyOptionTitleException, exception.Message);
        }

        [Fact]
        public async Task UpdateSurveyTestThrowsIdException()
        {
            survey.Id = Guid.Empty;

            mediator.Setup(m => m.Send(It.IsAny<UpdateSurveyCommand>(), It.IsAny<CancellationToken>()));
            mapper.Setup(m => m.Map<Survey>(surveyDto)).Returns(survey);

            ISurveysService surveyService =
                new SurveysService(mediator.Object, NullLoggerFactory.Instance, mapper.Object);

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () =>
                await surveyService.UpdateSurveyAsync(surveyDto, default));

            Assert.Equal(SurveyServiceStrings.UpdateSurveyIdException, exception.Message);
        }

        [Fact]
        public async Task UpdateSurveyTestThrowsQuestionIdException()
        {
            survey.Questions.First().Id = Guid.Empty;

            mediator.Setup(m => m.Send(It.IsAny<UpdateSurveyCommand>(), It.IsAny<CancellationToken>()));
            mapper.Setup(m => m.Map<Survey>(surveyDto)).Returns(survey);

            ISurveysService surveyService =
                new SurveysService(mediator.Object, NullLoggerFactory.Instance, mapper.Object);

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () =>
                await surveyService.UpdateSurveyAsync(surveyDto, default));

            Assert.Equal(SurveyServiceStrings.UpdateSurveyQuestionIdException, exception.Message);
        }

        [Fact]
        public async Task UpdateSurveyTestThrowsOptionIdException()
        {
            survey.Questions.First().Options.First().Id = Guid.Empty;

            mediator.Setup(m => m.Send(It.IsAny<UpdateSurveyCommand>(), It.IsAny<CancellationToken>()));
            mapper.Setup(m => m.Map<Survey>(surveyDto)).Returns(survey);

            ISurveysService surveyService =
                new SurveysService(mediator.Object, NullLoggerFactory.Instance, mapper.Object);

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () =>
                await surveyService.UpdateSurveyAsync(surveyDto, default));

            Assert.Equal(SurveyServiceStrings.UpdateSurveyOptionIdException, exception.Message);
        }

        [Fact]
        public async Task UpdateSurveyTestThrowsDateException()
        {
            survey.CreatedDate = new DateTime();

            mediator.Setup(m => m.Send(It.IsAny<UpdateSurveyCommand>(), It.IsAny<CancellationToken>()));
            mapper.Setup(m => m.Map<Survey>(surveyDto)).Returns(survey);

            ISurveysService surveyService =
                new SurveysService(mediator.Object, NullLoggerFactory.Instance, mapper.Object);

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () =>
                await surveyService.UpdateSurveyAsync(surveyDto, default));

            Assert.Equal(SurveyServiceStrings.UpdateSurveyDateException, exception.Message);
        }

        [Fact]
        public async Task UpdateSurveyTestMapperThrowsInnerException()
        {
            mapper.Setup(m => m.Map<Survey>(surveyDto)).Throws(new Exception());

            ISurveysService surveyService =
                new SurveysService(mediator.Object, NullLoggerFactory.Instance, mapper.Object);

            var exception =
                await Assert.ThrowsAsync<Exception>(async () =>
                    await surveyService.UpdateSurveyAsync(surveyDto, default));

            Assert.Equal(SurveyServiceStrings.UpdateSurveyException, exception.Message);
        }

        [Fact]
        public async Task UpdateSurveyTestThrowsInnerException()
        {
            mediator.Setup(m => m.Send(It.IsAny<UpdateSurveyCommand>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception());
            mapper.Setup(m => m.Map<Survey>(surveyDto)).Returns(survey);

            ISurveysService surveyService =
                new SurveysService(mediator.Object, NullLoggerFactory.Instance, mapper.Object);

            var exception =
                await Assert.ThrowsAsync<Exception>(async () =>
                    await surveyService.UpdateSurveyAsync(surveyDto, default));

            Assert.Equal(SurveyServiceStrings.UpdateSurveyException, exception.Message);
        }

        [Fact]
        public async Task UpdateSurveyTestThrowsArgumentExceptionNull()
        {
            mediator.Setup(m => m.Send(It.IsAny<UpdateSurveyCommand>(), It.IsAny<CancellationToken>()));
            mapper.Setup(m => m.Map<Survey>(surveyDto)).Returns(survey);
            ISurveysService surveyService =
                new SurveysService(mediator.Object, NullLoggerFactory.Instance, mapper.Object);

            var exception =
                await Assert.ThrowsAsync<ArgumentException>(async () =>
                    await surveyService.UpdateSurveyAsync(null, default));

            Assert.Equal(SurveyServiceStrings.UpdateSurveyNullException, exception.Message);
        }
    }
}