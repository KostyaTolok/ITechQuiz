using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.Answers;
using Application.DTO;
using Application.Interfaces.Services;
using Application.Queries.Answers;
using Application.Queries.Auth;
using Application.Queries.Surveys;
using AutoMapper;
using Domain.Entities.Surveys;
using Domain.Service;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services
{
    public class AnswersService : IAnswersService
    {
        private readonly ILogger<IAnswersService> logger;
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public AnswersService(IMediator mediator, IMapper mapper, ILoggerFactory factory)
        {
            this.mediator = mediator;
            this.mapper = mapper;
            logger = factory.CreateLogger<IAnswersService>();
        }

        public async Task AddAnswersAsync(IEnumerable<AnswerDTO> answerDtos, Guid? userId,
            CancellationToken token)
        {
            foreach (var answerDto in answerDtos ?? new List<AnswerDTO>())
            {
                if (answerDto.QuestionId == default)
                {
                    logger.LogError("{ExString}",
                        AnswerServiceStrings.AddAnswerQuestionIdException);

                    throw new ArgumentException(AnswerServiceStrings.AddAnswerQuestionIdException);
                }

                switch (answerDto.IsAnonymous)
                {
                    case true when !answerDto.IsAnonymousAllowed:
                        logger.LogError("{ExString}",
                            AnswerServiceStrings.AddAnswerAnonymousException);

                        throw new ArgumentException(AnswerServiceStrings.AddAnswerAnonymousException);
                    case false:
                    {
                        Survey survey;
                        try
                        {
                            survey = await mediator.Send(new GetSurveyByQuestionIdQuery(answerDto.QuestionId), token);
                        }
                        catch (Exception ex)
                        {
                            logger.LogError("{ExString} : {Ex}",
                                SurveyServiceStrings.GetSurveyException, ex.Message);

                            throw new ArgumentException(SurveyServiceStrings.GetSurveyException);
                        }

                        IEnumerable<Answer> previousAnswers;
                        try
                        {
                            previousAnswers = await mediator.Send(new GetAnswersQuery(survey.Id, userId), token);
                        }
                        catch (Exception ex)
                        {
                            logger.LogError("{ExString}: {Ex}",
                                AnswerServiceStrings.GetAnswersException, ex.Message);

                            throw new ArgumentException(AnswerServiceStrings.GetAnswersException);
                        }

                        if (!survey.IsMultipleAnswersAllowed && previousAnswers.Any())
                        {
                            logger.LogError("{ExString}",
                                AnswerServiceStrings.AddAnswerMultipleException);

                            throw new ArgumentException(AnswerServiceStrings.AddAnswerMultipleException);
                        }

                        break;
                    }
                }

                var answer = mapper.Map<Answer>(answerDto);
                answer.UserId = userId;

                if (answerDto.SelectedOptions.Any(option => option.Id == default))
                {
                    logger.LogError("{ExString}",
                        AnswerServiceStrings.AddAnswerOptionIdException);

                    throw new ArgumentException(AnswerServiceStrings.AddAnswerOptionIdException);
                }
                
                try
                {
                    await mediator.Send(new AddAnswerCommand(answer), token);
                }
                catch (Exception ex)
                {
                    logger.LogError("{ExString}: {Ex}",
                        AnswerServiceStrings.AddAnswerException, ex.Message);

                    throw new Exception(AnswerServiceStrings.AddAnswerException);
                }
            }
        }

        public async Task<IEnumerable<AnswerDTO>> GetAnswersAsync(Guid? surveyId,
            Guid? userId, CancellationToken token)
        {
            IEnumerable<Answer> answers;
            try
            {
                answers = await mediator.Send(new GetAnswersQuery(surveyId, userId), token);
            }
            catch (Exception ex)
            {
                logger.LogError("{ExString}: {Ex}",
                    AnswerServiceStrings.GetAnswersException, ex.Message);
                throw new Exception(AnswerServiceStrings.GetAnswersException);
            }

            if (answers != null)
            {
                return mapper.Map<IEnumerable<AnswerDTO>>(answers);
            }

            logger.LogError("{ExString}",
                AnswerServiceStrings.GetAnswersNullException);
            throw new Exception(AnswerServiceStrings.GetAnswersNullException);
        }
    }
}