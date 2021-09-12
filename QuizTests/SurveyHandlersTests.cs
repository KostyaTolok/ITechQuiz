using System;
using Xunit;
using Moq;
using ITechQuiz.Data.Interfaces;
using ITechQuiz.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using ITechQuiz.Service.Queries;
using ITechQuiz.Service.Commands;
using ITechQuiz.Service.Handlers;

namespace QuizTests
{
    public class SurveyHandlersTests
    {
        private readonly Mock<ISurveysRepository> surveysRepository = new Mock<ISurveysRepository>();

        [Fact]
        public async Task GetSurveysHandlerTest()
        {
            surveysRepository.Setup(m => m.GetSurveysAsync(default)).ReturnsAsync(GetTestSurveys()).Verifiable();

            GetSurveysHandler handler = new GetSurveysHandler(surveysRepository.Object);
            var actual = await handler.Handle(new GetSurveysQuery(), default);
            var expected = GetTestSurveys();

            surveysRepository.Verify();
            var actualSurveys = actual.ToList();

            actualSurveys.Should().BeEquivalentTo(expected, config: c => c.IgnoringCyclicReferences());
        }

        [Fact]
        public async Task GetSurveyByIdHandlerTest()
        {
            var expected = GetTestSurveys()[0];
            surveysRepository.Setup(m => m.GetSurveyAsync(expected.Id, default)).ReturnsAsync(expected).Verifiable();

            GetSurveyByIdHandler handler = new GetSurveyByIdHandler(surveysRepository.Object);
            var actual = await handler.Handle(new GetSurveyByIdQuery(expected.Id), default);

            surveysRepository.Verify();

            actual.Should().BeEquivalentTo(expected, config: c => c.IgnoringCyclicReferences());
        }

        [Fact]
        public async Task AddSurveyHandlerTest()
        {
            var testSurvey = GetTestSurveys()[0];
            surveysRepository.Setup(m => m.AddSurveyAsync(testSurvey, default)).Verifiable();

            AddSurveyHandler handler = new AddSurveyHandler(surveysRepository.Object);
            var actual = await handler.Handle(new AddSurveyCommand(testSurvey), default);

            surveysRepository.Verify();
        }

        [Fact]
        public async Task DeleteSurveyHandlerTest()
        {
            var testSurvey = GetTestSurveys()[0];
            surveysRepository.Setup(m => m.DeleteSurveyAsync(testSurvey.Id, default)).Verifiable();

            DeleteSurveyHandler handler = new DeleteSurveyHandler(surveysRepository.Object);
            var actual = await handler.Handle(new DeleteSurveyCommand(testSurvey.Id), default);

            surveysRepository.Verify();
        }

        [Fact]
        public async Task UpdateSurveyHandlerTest()
        {
            var testSurvey = GetTestSurveys()[0];
            surveysRepository.Setup(m => m.UpdateSurveyAsync(testSurvey, default)).Verifiable();

            UpdateSurveyHandler handler = new UpdateSurveyHandler(surveysRepository.Object);
            var actual = await handler.Handle(new UpdateSurveyCommand(testSurvey), default);

            surveysRepository.Verify();
        }

        private static List<Survey> GetTestSurveys()
        {
            Survey survey = new Survey()
            {
                Id = new Guid("827f753c-7544-463b-9fa4-d9c5c051cf17"),
                Name = "MySurv",
                CreatedDate = DateTime.Now.Date,
                Title = "Это опрос",
                Subtitle = "Спасибо за прохождение опроса",
                Type = SurveyType.ForStatistics,
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

            List<Survey> surveys = new List<Survey>()
            {
                survey
            };

            return surveys;
        }
    }
}
