﻿using System;
using Xunit;
using Moq;
using ITechQuiz.Data.Interfaces;
using ITechQuiz.Models;
using System.Collections.Generic;
using ITechQuiz.Data.Services;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using FluentAssertions;
using System.Threading;
using MediatR;
using ITechQuiz.Service.Queries;
using ITechQuiz.Service.Commands;

namespace QuizTests
{
    public class SurveyServiceTests
    {
        private readonly Mock<IMediator> mediator = new Mock<IMediator>();
        private readonly Mock<ILogger<SurveyService>> logger = new Mock<ILogger<SurveyService>>();


        [Fact]
        public async Task GetSurveysTest()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetSurveysQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(GetTestSurveys())
                .Verifiable();

            ISurveyService surveyService = new SurveyService(mediator.Object, logger.Object);

            var actual = await surveyService.GetSurveysAsync(default);
            var expected = GetTestSurveys();

            mediator.Verify();
            var actualSurveys = actual.ToList();

            actualSurveys.Should().BeEquivalentTo(expected, config: c => c.IgnoringCyclicReferences());
        }

        [Fact]
        public async Task GetSurveysTestTrowsArgumentException()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetSurveysQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((IEnumerable<Survey>)null)
                .Verifiable();

            ISurveyService surveyService = new SurveyService(mediator.Object, logger.Object);

            ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(async () => await surveyService.GetSurveysAsync(default));
            var expected = GetTestSurveys();

            mediator.Verify();

            Assert.Equal("Failed to get surveys", exception.Message);
        }

        [Fact]
        public async Task GetSurveyTest()
        {
            Survey testSurvey = GetTestSurveys()[0];

            mediator.Setup(m => m.Send(It.IsAny<GetSurveyByIdQuery>(), It.IsAny<CancellationToken>()))
               .ReturnsAsync(testSurvey)
               .Verifiable();

            ISurveyService surveyService = new SurveyService(mediator.Object, logger.Object);

            var actual = await surveyService.GetSurveyAsync(testSurvey.Id, default);

            mediator.Verify();

            actual.Should().BeEquivalentTo(testSurvey, config: c => c.IgnoringCyclicReferences());

        }

        [Fact]
        public async Task GetSurveyTestThrowsArgumentException()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetSurveyByIdQuery>(), It.IsAny<CancellationToken>()))
               .ReturnsAsync((Survey)null)
               .Verifiable();

            ISurveyService surveyService = new SurveyService(mediator.Object, logger.Object);

            ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(async () => await surveyService.GetSurveyAsync(new Guid(), default));

            mediator.Verify();

            Assert.Equal("Failed to get survey. Wrong name", exception.Message);

        }

        [Fact]
        public async Task AddSurveyTest()
        {
            Survey testSurvey = GetTestSurveys()[0];

            mediator.Setup(m => m.Send(It.IsAny<AddSurveyCommand>(), It.IsAny<CancellationToken>()))
               .Verifiable();

            ISurveyService surveyService = new SurveyService(mediator.Object, logger.Object);

            await surveyService.AddSurveyAsync(testSurvey, default);

            mediator.Verify();

        }

        [Fact]
        public async Task AddSurveyTestThrowsArgumentException()
        {
            Survey testSurvey = GetTestSurveys()[0];
            testSurvey.Name = "";

            mediator.Setup(m => m.Send(It.IsAny<AddSurveyCommand>(), It.IsAny<CancellationToken>()));

            ISurveyService surveyService = new SurveyService(mediator.Object, logger.Object);

            ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(async () => await surveyService.AddSurveyAsync(testSurvey, default));

            Assert.Equal("Failed to add survey. Missing required fields", exception.Message);
        }

        [Fact]
        public async Task AddSurveyTestThrowsArgumentExceptionNull()
        {
            Survey testSurvey = null;

            mediator.Setup(m => m.Send(It.IsAny<AddSurveyCommand>(), It.IsAny<CancellationToken>()));

            ISurveyService surveyService = new SurveyService(mediator.Object, logger.Object);

            ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(async () => await surveyService.AddSurveyAsync(testSurvey, default));

            Assert.Equal("Failed to add survey. Survey is null", exception.Message);
        }

        [Fact]
        public async Task DeleteSurveyTest()
        {
            Survey testSurvey = GetTestSurveys()[0];

            mediator.Setup(m => m.Send(It.IsAny<DeleteSurveyCommand>(), It.IsAny<CancellationToken>())).Verifiable();

            ISurveyService surveyService = new SurveyService(mediator.Object, logger.Object);

            await surveyService.DeleteSurveyAsync(testSurvey.Id, default);

            mediator.Verify();

        }

        [Fact]
        public async Task DeleteSurveyTestThrowsArgumentException()
        {
            Survey testSurvey = GetTestSurveys()[0];
            testSurvey.Id = new Guid();

            mediator.Setup(m => m.Send(It.IsAny<DeleteSurveyCommand>(), It.IsAny<CancellationToken>()));

            ISurveyService surveyService = new SurveyService(mediator.Object, logger.Object);

            ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(async () => await surveyService.DeleteSurveyAsync(testSurvey.Id, default));

            Assert.Equal("Failed to delete survey. Empty name", exception.Message);
        }

        [Fact]
        public async Task UpdateSurveyTest()
        {
            Survey testSurvey = GetTestSurveys()[0];

            mediator.Setup(m => m.Send(It.IsAny<UpdateSurveyCommand>(), It.IsAny<CancellationToken>())).Verifiable();

            ISurveyService surveyService = new SurveyService(mediator.Object, logger.Object);

            await surveyService.UpdateSurveyAsync(testSurvey, default);

            mediator.Verify();

        }

        [Fact]
        public async Task UpdateSurveyTestThrowsArgumentException()
        {
            Survey testSurvey = GetTestSurveys()[0];
            testSurvey.Title = "";

            mediator.Setup(m => m.Send(It.IsAny<UpdateSurveyCommand>(), It.IsAny<CancellationToken>()));

            ISurveyService surveyService = new SurveyService(mediator.Object, logger.Object);

            ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(async () => await surveyService.UpdateSurveyAsync(testSurvey, default));

            Assert.Equal("Failed to update survey. Missing required fields", exception.Message);
        }

        [Fact]
        public async Task UpdateSurveyTestThrowsArgumentExceptionNull()
        {
            Survey testSurvey = null;

            mediator.Setup(m => m.Send(It.IsAny<UpdateSurveyCommand>(), It.IsAny<CancellationToken>()));

            ISurveyService surveyService = new SurveyService(mediator.Object, logger.Object);

            ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(async () => await surveyService.UpdateSurveyAsync(testSurvey, default));

            Assert.Equal("Failed to update survey. Survey is null", exception.Message);
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