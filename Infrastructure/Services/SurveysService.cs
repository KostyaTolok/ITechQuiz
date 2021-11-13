using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.Surveys;
using Application.DTO;
using Application.Interfaces.Services;
using Application.Queries.Auth;
using Application.Queries.Surveys;
using AutoMapper;
using Domain.Entities.Surveys;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Service;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;

namespace Infrastructure.Services
{
    public class SurveysService : ISurveysService
    {
        private readonly ILogger<ISurveysService> logger;
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public SurveysService(IMediator mediator, ILoggerFactory factory, IMapper mapper)
        {
            logger = factory.CreateLogger<ISurveysService>();
            this.mediator = mediator;
            this.mapper = mapper;
        }

        public async Task UpdateSurveyAsync(SurveyDTO surveyDTO, CancellationToken token)
        {
            Survey survey;
            try
            {
                survey = mapper.Map<Survey>(surveyDTO);
            }
            catch (Exception ex)
            {
                logger.LogError("{ExString}: {Ex}",
                    SurveyServiceStrings.UpdateSurveyException, ex.Message);

                throw new Exception(SurveyServiceStrings.UpdateSurveyException);
            }

            if (survey == null)
            {
                logger.LogError(SurveyServiceStrings.UpdateSurveyNullException);
                throw new ArgumentException(SurveyServiceStrings.UpdateSurveyNullException);
            }

            if (survey.Id == default)
            {
                logger.LogError(SurveyServiceStrings.UpdateSurveyIdException);
                throw new ArgumentException(SurveyServiceStrings.UpdateSurveyIdException);
            }
            else if (survey.CreatedDate == default)
            {
                logger.LogError(SurveyServiceStrings.UpdateSurveyDateException);
                throw new ArgumentException(SurveyServiceStrings.UpdateSurveyDateException);
            }
            else if (string.IsNullOrEmpty(survey.Title))
            {
                logger.LogError(SurveyServiceStrings.UpdateSurveyTitleException);
                throw new ArgumentException(SurveyServiceStrings.UpdateSurveyTitleException);
            }

            foreach (var question in survey.Questions)
            {
                if (string.IsNullOrEmpty(question.Title))
                {
                    logger.LogError(SurveyServiceStrings.UpdateSurveyQuestionTitleException);
                    throw new ArgumentException(SurveyServiceStrings.UpdateSurveyQuestionTitleException);
                }
                else if (question.Id == default)
                {
                    logger.LogError(SurveyServiceStrings.UpdateSurveyQuestionIdException);
                    throw new ArgumentException(SurveyServiceStrings.UpdateSurveyQuestionIdException);
                }

                foreach (var option in question.Options)
                {
                    if (string.IsNullOrEmpty(option.Title))
                    {
                        logger.LogError(SurveyServiceStrings.UpdateSurveyOptionTitleException);
                        throw new ArgumentException(SurveyServiceStrings.UpdateSurveyOptionTitleException);
                    }
                    else if(option.Id == default)
                    {
                        logger.LogError(SurveyServiceStrings.UpdateSurveyOptionIdException);
                        throw new ArgumentException(SurveyServiceStrings.UpdateSurveyOptionIdException);
                    }
                }
                
            }

            try
            {
                await mediator.Send(new UpdateSurveyCommand(survey), token);
            }
            catch (Exception ex)
            {
                logger.LogError("{ExString}: {Ex}", SurveyServiceStrings.UpdateSurveyException,
                    ex.Message);

                throw new Exception(SurveyServiceStrings.UpdateSurveyException);
            }
        }

        public async Task<Guid> AddSurveyAsync(SurveyDTO surveyDTO,
            string userEmail, CancellationToken token)
        {
            if (surveyDTO == null)
            {
                logger.LogError(SurveyServiceStrings.AddSurveyNullException);
                throw new ArgumentException(SurveyServiceStrings.AddSurveyNullException);
            }
            
            Survey survey;
            try
            {
                survey = mapper.Map<Survey>(surveyDTO);
            }
            catch (Exception ex)
            {
                logger.LogError("{ExString}: {Ex}",
                    SurveyServiceStrings.AddSurveyException, ex.Message);

                throw new Exception(SurveyServiceStrings.AddSurveyException);
            }

            Guid userId;
            try
            {
                var user = await mediator.Send(new GetUserByEmailQuery(userEmail), token);

                userId = user.Id;
            }
            catch (Exception ex)
            {
                logger.LogError("{ExString}: {Ex}",
                    UserServiceStrings.GetUserException, ex.Message);

                throw new Exception(UserServiceStrings.GetUserException);
            }

            if (string.IsNullOrEmpty(survey.Title))
            {
                logger.LogError(SurveyServiceStrings.AddSurveyTitleException);
                throw new ArgumentException(SurveyServiceStrings.AddSurveyTitleException);
            }

            foreach (var question in survey.Questions)
            {
                if (string.IsNullOrEmpty(question.Title))
                {
                    logger.LogError(SurveyServiceStrings.AddSurveyQuestionTitleException);
                    throw new ArgumentException(SurveyServiceStrings.AddSurveyQuestionTitleException);
                }

                if (!question.Options.Any(option => string.IsNullOrEmpty(option.Title))) continue;
                logger.LogError(SurveyServiceStrings.AddSurveyOptionTitleException);
                throw new ArgumentException(SurveyServiceStrings.AddSurveyOptionTitleException);
            }

            try
            {
                survey.CreatedDate = DateTime.Now;
                survey.UserId = userId;
                return await mediator.Send(new AddSurveyCommand(survey), token);
            }
            catch (Exception ex)
            {
                logger.LogError
                    ("{ExString}: {Ex}", SurveyServiceStrings.AddSurveyException, ex.Message);
                throw new Exception(SurveyServiceStrings.AddSurveyException);
            }
        }

        public async Task<bool> DeleteSurveyAsync(Guid id, CancellationToken token)
        {
            if (id == default)
            {
                logger.LogError(SurveyServiceStrings.DeleteSurveyIdException);
                throw new ArgumentException(SurveyServiceStrings.DeleteSurveyIdException);
            }

            try
            {
                return await mediator.Send(new DeleteSurveyCommand(id), token);
            }
            catch (Exception ex)
            {
                logger.LogError("{ExString}: {Ex}",
                    SurveyServiceStrings.DeleteSurveyException, ex.Message);
                throw new Exception(SurveyServiceStrings.DeleteSurveyException);
            }
        }

        public async Task<SurveyDTO> GetSurveyAsync(Guid id, CancellationToken token)
        {
            Survey survey;
            try
            {
                survey = await mediator.Send(new GetSurveyByIdQuery(id), token);
            }
            catch (Exception ex)
            {
                logger.LogError("{ExString}: {Ex}", SurveyServiceStrings.GetSurveyException, ex);
                throw new Exception(SurveyServiceStrings.GetSurveyException);
            }

            if (survey != null)
            {
                return mapper.Map<SurveyDTO>(survey);
            }

            logger.LogError(SurveyServiceStrings.GetSurveyIdException);
            throw new ArgumentException(SurveyServiceStrings.GetSurveyIdException);
        }

        public async Task<IEnumerable<SurveyDTO>> GetSurveysAsync(Guid? userId,
            string type, CancellationToken token)
        {
            IEnumerable<Survey> surveys;
            try
            {
                SurveyTypes? surveyType =
                    Enum.TryParse(type, true, out SurveyTypes result) ? result : null;

                surveys = await mediator.Send(new GetSurveysQuery(userId, surveyType), token);
            }
            catch (Exception ex)
            {
                logger.LogError
                    ("{ExString}: {Ex}", SurveyServiceStrings.GetSurveysException, ex.Message);
                throw new Exception(SurveyServiceStrings.GetSurveysException);
            }

            if (surveys != null)
            {
                return mapper.Map<IEnumerable<SurveyDTO>>(surveys);
            }

            logger.LogError(SurveyServiceStrings.GetSurveysNullException);
            throw new ArgumentException(SurveyServiceStrings.GetSurveysNullException);
        }
    }
}