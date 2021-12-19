using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Application.DTO;
using Application.Interfaces.Services;
using Application.Queries.Questions;
using AutoMapper;
using Domain.Entities.Surveys;
using Domain.Service;
using FluentAssertions;
using Infrastructure.Services;
using MediatR;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Xunit;

namespace Application.UnitTests
{
    public class StatisticsServiceTests
    {
        private readonly Mock<IMediator> mediator = new();
        private readonly Mock<IMapper> mapper = new();
        private readonly IEnumerable<QuestionStatisticsDTO> statistics = TestData.GetTestStatistics();
        private readonly IEnumerable<Question> questions = TestData.GetTestSurveys()[0].Questions;

        [Fact]
        public async void GetStatisticsTest()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetQuestionsBySurveyId>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(questions)
                .Verifiable();
            mapper.Setup(m => m.Map<IEnumerable<QuestionStatisticsDTO>>(It.IsAny<IEnumerable<Question>>()))
                .Returns(statistics);
            
            IStatisticsService statisticsService =
                new StatisticsService(mediator.Object, mapper.Object, NullLoggerFactory.Instance);

            var actual = 
                await statisticsService.GetStatisticsAsync(Guid.NewGuid(), CancellationToken.None);

            mediator.VerifyAll();
            var actualStatistics = actual.ToList();

            actualStatistics.Should().BeEquivalentTo(statistics, c => c.IgnoringCyclicReferences());
        }
        
        [Fact]
        public async void GetStatisticsThrowsIdExceptionTest()
        {
            IStatisticsService statisticsService =
                new StatisticsService(mediator.Object, mapper.Object, NullLoggerFactory.Instance);

            var exception = await Assert.ThrowsAsync<ArgumentException>
                (async () => await statisticsService.GetStatisticsAsync(default, CancellationToken.None));

            Assert.Equal(QuestionServiceStrings.GetQuestionsIdException, exception.Message);
        }
        
        [Fact]
        public async void GetStatisticsThrowsMapperExceptionTest()
        {
            mapper.Setup(m => m.Map<IEnumerable<QuestionStatisticsDTO>>(It.IsAny<IEnumerable<Question>>()))
                .Throws<AutoMapperMappingException>();
            IStatisticsService statisticsService =
                new StatisticsService(mediator.Object, mapper.Object, NullLoggerFactory.Instance);

            var exception = await Assert.ThrowsAsync<Exception>
                (async () => await statisticsService.GetStatisticsAsync(Guid.NewGuid(), CancellationToken.None));

            Assert.Equal(QuestionServiceStrings.GetQuestionsException, exception.Message);
        }
        
        [Fact]
        public async void GetStatisticsThrowsExceptionTest()
        {
            mediator.Setup(m => m.Send(It.IsAny<GetQuestionsBySurveyId>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception())
                .Verifiable();
            mapper.Setup(m => m.Map<IEnumerable<QuestionStatisticsDTO>>(It.IsAny<IEnumerable<Question>>()))
                .Throws(new AutoMapperMappingException());
            IStatisticsService statisticsService =
                new StatisticsService(mediator.Object, mapper.Object, NullLoggerFactory.Instance);

            var exception = await Assert.ThrowsAsync<Exception>
                (async () => await statisticsService.GetStatisticsAsync(Guid.NewGuid(), CancellationToken.None));

            Assert.Equal(QuestionServiceStrings.GetQuestionsException, exception.Message);
        }
    }
}