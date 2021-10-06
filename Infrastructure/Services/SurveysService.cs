using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.Surveys;
using Application.DTO;
using Application.Interfaces.Services;
using Application.Queries.Surveys;
using AutoMapper;
using Domain.Entities.Surveys;
using Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

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
                logger.LogError("Error occured while updating survey: {Ex}", ex);
                throw new Exception("An internal error occured while updating survey");
            }

            if (survey == null)
            {
                logger.LogError("Failed to update survey. Survey is null");
                throw new ArgumentNullException("Failed to update survey. Survey is null");
            }

            if (survey.Id == default)
            {
                logger.LogError("Failed to update survey. Missing id");
                throw new ArgumentException("Failed to update survey. Missing id");
            }
            else if (string.IsNullOrEmpty(survey.Name))
            {
                logger.LogError("Failed to update survey. Missing Name");
                throw new ArgumentException("Failed to update survey. Missing Name");
            }
            else if (survey.CreatedDate == default)
            {
                logger.LogError("Failed to update survey. Missing date of creation");
                throw new ArgumentException("Failed to update survey. Missing date of creation");
            }
            else if (string.IsNullOrEmpty(survey.Title))
            {
                logger.LogError("Failed to update survey. Missing required title");
                throw new ArgumentException("Failed to update survey. Missing title");
            }

            try
            {
                await mediator.Send(new UpdateSurveyCommand(survey), token);
            }
            catch (Exception ex)
            {
                logger.LogError("Error occured while updating survey: {Ex}", ex);
                throw new Exception("An internal error occured while updating survey");
            }
        }

        public async Task<Guid> AddSurveyAsync(SurveyDTO surveyDTO, CancellationToken token)
        {
            var survey = mapper.Map<Survey>(surveyDTO);
            if (survey == null)
            {
                logger.LogError("Failed to add survey. Survey is null");
                throw new ArgumentNullException("Failed to add survey. Survey is null");
            }

            if (string.IsNullOrEmpty(survey.Name))
            {
                logger.LogError("Failed to add survey. Missing Name");
                throw new ArgumentException("Failed to add survey. Missing Name");
            }
            else if (survey.CreatedDate == default)
            {
                logger.LogError("Failed to add survey. Missing date of creation");
                throw new ArgumentException("Failed to add survey. Missing date of creation");
            }
            else if (string.IsNullOrEmpty(survey.Title))
            {
                logger.LogError("Failed to add survey. Missing required title");
                throw new ArgumentException("Failed to add survey. Missing title");
            }

            try
            {
                return await mediator.Send(new AddSurveyCommand(survey), token);
            }
            catch (Exception ex)
            {
                logger.LogError("Error occured while adding survey: {Ex}", ex);
                throw new Exception("An internal error occured while adding survey");
            }
        }

        public async Task<bool> DeleteSurveyAsync(Guid id, CancellationToken token)
        {
            if (id == default)
            {
                logger.LogError("Failed to delete survey. Wrong id");
                throw new ArgumentException("Failed to delete survey. Wrong id");
            }

            try
            {
                return await mediator.Send(new DeleteSurveyCommand(id), token);
            }
            catch (Exception ex)
            {
                logger.LogError("Error occured while deleting survey: {Ex}", ex);
                throw new Exception("An internal error occured while deleting survey");
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
                logger.LogError("Error occured while getting survey: {Ex}", ex);
                throw new Exception("An internal error occured while getting survey");
            }

            if (survey != null)
            {
                return mapper.Map<SurveyDTO>(survey);
            }

            logger.LogError("Failed to get survey. Wrong id");
            throw new ArgumentException("Failed to get survey. Wrong id");
        }

        public async Task<IEnumerable<SurveyDTO>> GetSurveysAsync(Guid? userId, CancellationToken token)
        {
            IEnumerable<Survey> surveys;
            try
            {
                if (userId.HasValue)
                {
                    surveys = await mediator.Send(new GetSurveysByUserIdQuery(userId.Value), token);
                }
                else
                {
                    surveys = await mediator.Send(new GetSurveysQuery(), token);
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error occured while getting surveys: {Ex}", ex);
                throw new Exception("An internal error occured while getting surveys");
            }

            if (surveys != null)
            {
                return mapper.Map<IEnumerable<SurveyDTO>>(surveys);
            }

            logger.LogError("Failed to get surveys");
            throw new BusinessLogicException("Failed to get surveys");
        }

    }
}
