using System;
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

namespace Application.UnitTests
{
    public class SurveyServiceTests
    {
        private readonly Mock<IMediator> mediator = new();
        private readonly IEnumerable<Survey> surveys = TestData.GetTestSurveys();
        private readonly Survey survey = TestData.GetTestSurveys()[0];

        [Fact]
        public async Task GetSurveysTest()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetSurveysQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(surveys)
                .Verifiable();

            ISurveyService surveyService = new SurveyService(mediator.Object, NullLoggerFactory.Instance);

            var actual = await surveyService.GetSurveysAsync(default);

            mediator.Verify();
            var actualSurveys = actual.ToList();

            actualSurveys.Should().BeEquivalentTo(surveys, config: c => c.IgnoringCyclicReferences());
        }

        [Fact]
        public async Task GetSurveysTestTrowsArgumentException()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetSurveysQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((IEnumerable<Survey>)null)
                .Verifiable();

            ISurveyService surveyService = new SurveyService(mediator.Object, NullLoggerFactory.Instance);

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await surveyService.GetSurveysAsync(default));

            mediator.Verify();

            Assert.Equal("Failed to get surveys", exception.Message);
        }

        [Fact]
        public async Task GetSurveysByUserIdTest()
        {
            var userId = new Guid("78f33f64-29cc-4b47-82ab-bffd90c177a2");
            var expected = surveys.Where(survey => survey.UserId == userId);

            mediator.Setup(m => m.Send(It.IsAny<GetSurveysByUserIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expected)
                .Verifiable();

            ISurveyService surveyService = new SurveyService(mediator.Object, NullLoggerFactory.Instance);

            var actual = await surveyService.GetSurveysByUserIdAsync(userId, default);

            mediator.Verify();
            var actualSurveys = actual.ToList();

            actualSurveys.Should().BeEquivalentTo(expected, config: c => c.IgnoringCyclicReferences());
        }

        [Fact]
        public async Task GetSurveysByUserIdTestTrowsArgumentException()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetSurveysByUserIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((IEnumerable<Survey>)null)
                .Verifiable();

            ISurveyService surveyService = new SurveyService(mediator.Object, NullLoggerFactory.Instance);

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await surveyService.GetSurveysByUserIdAsync(new Guid(), default));

            mediator.Verify();

            Assert.Equal("Failed to get surveys", exception.Message);
        }

        [Fact]
        public async Task GetSurveyTest()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetSurveyByIdQuery>(), It.IsAny<CancellationToken>()))
               .ReturnsAsync(survey)
               .Verifiable();

            ISurveyService surveyService = new SurveyService(mediator.Object, NullLoggerFactory.Instance);

            var actual = await surveyService.GetSurveyAsync(survey.Id, default);

            mediator.Verify();

            actual.Should().BeEquivalentTo(survey, config: c => c.IgnoringCyclicReferences());

        }

        [Fact]
        public async Task GetSurveyTestThrowsArgumentException()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetSurveyByIdQuery>(), It.IsAny<CancellationToken>()))
               .ReturnsAsync((Survey)null)
               .Verifiable();

            ISurveyService surveyService = new SurveyService(mediator.Object, NullLoggerFactory.Instance);

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await surveyService.GetSurveyAsync(new Guid(), default));

            mediator.Verify();

            Assert.Equal("Failed to get survey. Wrong id", exception.Message);

        }

        [Fact]
        public async Task AddSurveyTest()
        {
            mediator.Setup(m => m.Send(It.IsAny<AddSurveyCommand>(), It.IsAny<CancellationToken>()))
               .Verifiable();

            ISurveyService surveyService = new SurveyService(mediator.Object, NullLoggerFactory.Instance);

            await surveyService.AddSurveyAsync(survey, default);

            mediator.Verify();

        }

        [Fact]
        public async Task AddSurveyTestThrowsArgumentException()
        {
            survey.Name = "";

            mediator.Setup(m => m.Send(It.IsAny<AddSurveyCommand>(), It.IsAny<CancellationToken>()));

            ISurveyService surveyService = new SurveyService(mediator.Object, NullLoggerFactory.Instance);

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await surveyService.AddSurveyAsync(survey, default));

            Assert.Equal("Failed to add survey. Missing required fields", exception.Message);
        }

        [Fact]
        public async Task AddSurveyTestThrowsArgumentExceptionNull()
        {
            mediator.Setup(m => m.Send(It.IsAny<AddSurveyCommand>(), It.IsAny<CancellationToken>()));

            ISurveyService surveyService = new SurveyService(mediator.Object, NullLoggerFactory.Instance);

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await surveyService.AddSurveyAsync(null, default));

            Assert.Equal("Failed to add survey. Survey is null", exception.Message);
        }

        [Fact]
        public async Task DeleteSurveyTest()
        {
            mediator.Setup(m => m.Send(It.IsAny<DeleteSurveyCommand>(), It.IsAny<CancellationToken>())).Verifiable();

            ISurveyService surveyService = new SurveyService(mediator.Object, NullLoggerFactory.Instance);

            await surveyService.DeleteSurveyAsync(survey.Id, default);

            mediator.Verify();

        }

        [Fact]
        public async Task DeleteSurveyTestThrowsArgumentException()
        {
            survey.Id = new Guid();

            mediator.Setup(m => m.Send(It.IsAny<DeleteSurveyCommand>(), It.IsAny<CancellationToken>()));

            ISurveyService surveyService = new SurveyService(mediator.Object, NullLoggerFactory.Instance);

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await surveyService.DeleteSurveyAsync(survey.Id, default));

            Assert.Equal("Failed to delete survey. Wrong id", exception.Message);
        }

        [Fact]
        public async Task UpdateSurveyTest()
        {
            mediator.Setup(m => m.Send(It.IsAny<UpdateSurveyCommand>(), It.IsAny<CancellationToken>())).Verifiable();

            ISurveyService surveyService = new SurveyService(mediator.Object, NullLoggerFactory.Instance);

            await surveyService.UpdateSurveyAsync(survey, default);

            mediator.Verify();

        }

        [Fact]
        public async Task UpdateSurveyTestThrowsArgumentException()
        {
            survey.Title = "";

            mediator.Setup(m => m.Send(It.IsAny<UpdateSurveyCommand>(), It.IsAny<CancellationToken>()));

            ISurveyService surveyService = new SurveyService(mediator.Object, NullLoggerFactory.Instance);

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await surveyService.UpdateSurveyAsync(survey, default));

            Assert.Equal("Failed to update survey. Missing required fields", exception.Message);
        }

        [Fact]
        public async Task UpdateSurveyTestThrowsArgumentExceptionNull()
        {
            mediator.Setup(m => m.Send(It.IsAny<UpdateSurveyCommand>(), It.IsAny<CancellationToken>()));

            ISurveyService surveyService = new SurveyService(mediator.Object, NullLoggerFactory.Instance);

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await surveyService.UpdateSurveyAsync(null, default));

            Assert.Equal("Failed to update survey. Survey is null", exception.Message);
        }

    }
}
