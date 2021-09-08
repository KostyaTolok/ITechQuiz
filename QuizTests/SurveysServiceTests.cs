using System;
using Xunit;
using Moq;
using ITechQuiz.Data.Interfaces;
using ITechQuiz.Models;
using System.Collections.Generic;
using ITechQuiz.Data.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ITechQuiz.Data;
using FluentAssertions;

namespace QuizTests
{
    public class SurveyServiceTests
    {
        private readonly Mock<ISurveysRepository> surveysRepository = new Mock<ISurveysRepository>();
        private readonly Mock<ILogger<SurveyService>> logger = new Mock<ILogger<SurveyService>>();


        [Fact]
        public async Task GetSurveysTest()
        {

            surveysRepository.Setup(m => m.GetSurveysAsync()).ReturnsAsync(GetTestSurveys()).Verifiable();

            ISurveyService surveyService = new SurveyService(surveysRepository.Object, logger.Object);

            var actual = await surveyService.GetSurveysAsync();
            var expected = GetTestSurveys();

            surveysRepository.Verify();
            var actualSurveys = actual.ToList();

            actualSurveys.Should().BeEquivalentTo(expected, config: c => c.IgnoringCyclicReferences());
        }

        [Fact]
        public async Task GetSurveysTestTrowsArgumentException()
        {

            surveysRepository.Setup(m => m.GetSurveysAsync()).ReturnsAsync((List<Survey>)null).Verifiable();

            ISurveyService surveyService = new SurveyService(surveysRepository.Object, logger.Object);

            ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(async () => await surveyService.GetSurveysAsync());
            var expected = GetTestSurveys();

            surveysRepository.Verify();

            Assert.Equal("Failed to get surveys", exception.Message);
        }

        [Fact]
        public async Task GetSurveyTest()
        {
            Survey testSurvey = GetTestSurveys()[0];

            surveysRepository.Setup(m => m.GetSurveyAsync(testSurvey.Name)).ReturnsAsync(testSurvey).Verifiable();
            ISurveyService surveyService = new SurveyService(surveysRepository.Object, logger.Object);

            var actual = await surveyService.GetSurveyAsync(testSurvey.Name);

            surveysRepository.Verify();

            actual.Should().BeEquivalentTo(testSurvey, config: c => c.IgnoringCyclicReferences());

        }

        [Fact]
        public async Task GetSurveyTestThrowsArgumentException()
        {

            surveysRepository.Setup(m => m.GetSurveyAsync("")).ReturnsAsync((Survey)null).Verifiable();
            ISurveyService surveyService = new SurveyService(surveysRepository.Object, logger.Object);

            ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(async () => await surveyService.GetSurveyAsync(""));

            surveysRepository.Verify();

            Assert.Equal("Failed to get survey. Wrong name", exception.Message);

        }

        [Fact]
        public async Task AddSurveyTest()
        {
            Survey testSurvey = GetTestSurveys()[0];

            surveysRepository.Setup(m => m.AddSurveyAsync(testSurvey)).Verifiable();
            ISurveyService surveyService = new SurveyService(surveysRepository.Object, logger.Object);

            await surveyService.AddSurveyAsync(testSurvey);

            surveysRepository.Verify();

        }

        [Fact]
        public async Task AddSurveyTestThrowsArgumentException()
        {
            Survey testSurvey = GetTestSurveys()[0];
            testSurvey.Name = "";

            surveysRepository.Setup(m => m.AddSurveyAsync(testSurvey));
            ISurveyService surveyService = new SurveyService(surveysRepository.Object, logger.Object);

            ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(async () => await surveyService.AddSurveyAsync(testSurvey));

            Assert.Equal("Failed to add survey. Missing required fields", exception.Message);

        }

        [Fact]
        public async Task DeleteSurveyTest()
        {
            Survey testSurvey = GetTestSurveys()[0];

            surveysRepository.Setup(m => m.DeleteSurveyAsync(testSurvey.Name)).Verifiable();
            ISurveyService surveyService = new SurveyService(surveysRepository.Object, logger.Object);

            await surveyService.DeleteSurveyAsync(testSurvey.Name);

            surveysRepository.Verify();

        }

        [Fact]
        public async Task DeleteSurveyTestThrowsArgumentException()
        {
            Survey testSurvey = GetTestSurveys()[0];
            testSurvey.Name = "";
            surveysRepository.Setup(m => m.DeleteSurveyAsync(testSurvey.Name)).Verifiable();
            ISurveyService surveyService = new SurveyService(surveysRepository.Object, logger.Object);

            ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(async () => await surveyService.DeleteSurveyAsync(testSurvey.Name));

            Assert.Equal("Failed to delete survey. Empty name", exception.Message);

        }

        [Fact]
        public async Task UpdateSurveyTest()
        {
            Survey testSurvey = GetTestSurveys()[0];

            surveysRepository.Setup(m => m.UpdateSurveyAsync(testSurvey)).Verifiable();
            ISurveyService surveyService = new SurveyService(surveysRepository.Object, logger.Object);

            await surveyService.UpdateSurveyAsync(testSurvey);

            surveysRepository.Verify();

        }

        [Fact]
        public async Task UpdateSurveyTestThrowsArgumentException()
        {
            Survey testSurvey = GetTestSurveys()[0];
            testSurvey.Title = "";
            surveysRepository.Setup(m => m.UpdateSurveyAsync(testSurvey)).Verifiable();
            ISurveyService surveyService = new SurveyService(surveysRepository.Object, logger.Object);

            ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(async () => await surveyService.UpdateSurveyAsync(testSurvey));

            Assert.Equal("Failed to update survey. Missing required fields", exception.Message);

        }

        private static List<Survey> GetTestSurveys()
        {
            Survey survey = new Survey()
            {
                Name = "MySurv",
                CreatedDate = DateTime.Now.Date,
                Title = "Это опрос",
                Subtitle = "Спасибо за прохождение опроса",
                Type = SurveyType.ForStatistics,
                Questions = new List<Question>()
            };
            Question question = new Question()
            {
                Id = new Guid("bc6d54ae-5da2-477f-a02f-fbff4c73a638"),
                Title = "Первый вопрос",
                Multiple = false,
                MaxSelected = 1,
                Required = false,
                SurveyName = "MySurv",
                Options = new List<Option>(),
                Survey = survey
            };
            survey.Questions.Add(question);
            Option option = new Option()
            {
                Id = new Guid("bc6d54ae-5da2-477f-a02f-fbff4c73a438"),
                Title = "Это вариант",
                IsCorrect = true,
                Subtitle = "Блаблабла",
                QuestionId = new Guid("bc6d54ae-5da2-477f-a02f-fbff4c73a638"),
                Question = question
            };
            question.Options.Add(option);
            List<Survey> surveys = new List<Survey>()
            {
                survey
            };

            return surveys;
        }

    }



}
