using System;
using Xunit;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Domain.Entities.Surveys;
using Infrastructure.Handlers.Surveys;
using Application.Queries.Surveys;
using Application.Commands.Surveys;
using Application.Interfaces.Repositories;

namespace Application.UnitTests
{
    public class SurveyHandlersTests
    {
        private readonly Mock<ISurveysRepository> surveysRepository = new();
        private readonly IEnumerable<Survey> surveys = TestData.GetTestSurveys();
        private readonly Survey survey = TestData.GetTestSurveys()[0];

        [Fact]
        public async Task GetSurveysHandlerTest()
        {
            surveysRepository
                .Setup(m => m.GetSurveysAsync(null, null, CancellationToken.None)).ReturnsAsync(surveys).Verifiable();

            var handler = new GetSurveysHandler(surveysRepository.Object);
            var actual = await handler.Handle(new GetSurveysQuery(null, null), CancellationToken.None);

            surveysRepository.Verify();
            var actualSurveys = actual.ToList();

            actualSurveys.Should().BeEquivalentTo(surveys, c => c.IgnoringCyclicReferences());
        }

        [Fact]
        public async Task GetSurveyByIdHandlerTest()
        {
            surveysRepository.Setup(m => m.GetSurveyAsync(survey.Id, default)).ReturnsAsync(survey).Verifiable();

            var handler = new GetSurveyByIdHandler(surveysRepository.Object);
            var actual = await handler.Handle(new GetSurveyByIdQuery(survey.Id), default);

            surveysRepository.Verify();

            actual.Should().BeEquivalentTo(survey, c => c.IgnoringCyclicReferences());
        }

        [Fact]
        public async Task AddSurveyHandlerTest()
        {
            surveysRepository.Setup(m => m.AddSurveyAsync(survey, default)).Verifiable();

            var handler = new AddSurveyHandler(surveysRepository.Object);
            var actual = await handler.Handle(new AddSurveyCommand(survey), default);

            surveysRepository.Verify();
        }

        [Fact]
        public async Task DeleteSurveyHandlerTest()
        {
            surveysRepository.Setup(m => m.DeleteSurveyAsync(survey, default)).Verifiable();
            surveysRepository
                .Setup(m => m.GetSurveyAsync(survey.Id, default)).
                ReturnsAsync(survey).Verifiable();
            
            var handler = new DeleteSurveyHandler(surveysRepository.Object);
            await handler.Handle(new DeleteSurveyCommand(survey.Id), default);

            surveysRepository.Verify();
        }

        [Fact]
        public async Task UpdateSurveyHandlerTest()
        {
            surveysRepository.Setup(m => m.UpdateSurveyAsync(survey, default)).Verifiable();

            var handler = new UpdateSurveyHandler(surveysRepository.Object);
            var actual = await handler.Handle(new UpdateSurveyCommand(survey), default);

            surveysRepository.Verify();
        }
    }
}