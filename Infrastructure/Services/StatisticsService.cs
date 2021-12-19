using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.DTO;
using Application.Interfaces.Services;
using Application.Queries.Questions;
using AutoMapper;
using Domain.Entities.Surveys;
using Domain.Service;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;
        private readonly ILogger<StatisticsService> logger;

        public StatisticsService(IMediator mediator, IMapper mapper, ILoggerFactory factory)
        {
            this.mediator = mediator;
            this.mapper = mapper;
            logger = factory.CreateLogger<StatisticsService>();
        }

        public async Task<IEnumerable<QuestionStatisticsDTO>> GetStatisticsAsync(Guid surveyId,
            CancellationToken token)
        {
            if (surveyId == default)
            {
                logger.LogError("{ExString}",
                    QuestionServiceStrings.GetQuestionsIdException);
                throw new ArgumentException(QuestionServiceStrings.GetQuestionsIdException);
            }
            try
            {
                var questions = await mediator.Send(new GetQuestionsBySurveyId(surveyId), token);
                return mapper.Map<IEnumerable<QuestionStatisticsDTO>>(questions);
            }
            catch (AutoMapperMappingException ex)
            {
                logger.LogError("{ExString}: {Ex}",
                    QuestionServiceStrings.GetQuestionsException, ex.Message);
                throw new Exception(QuestionServiceStrings.GetQuestionsException);
            }
            catch (Exception ex)
            {
                logger.LogError("{ExString}: {Ex}",
                    QuestionServiceStrings.GetQuestionsException, ex.Message);
                throw new Exception(QuestionServiceStrings.GetQuestionsException);
            }
        }
    }
}