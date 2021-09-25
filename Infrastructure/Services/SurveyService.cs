﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.Surveys;
using Application.Interfaces.Services;
using Application.Queries.Surveys;
using Domain.Entities.Surveys;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services
{
    public class SurveyService : ISurveyService
    {
        private readonly ILogger<ISurveyService> logger;
        private readonly IMediator mediator;

        public SurveyService(IMediator mediator, ILoggerFactory factory)
        {
            logger = factory.CreateLogger<ISurveyService>();
            this.mediator = mediator;
        }

        public async Task UpdateSurveyAsync(Survey survey, CancellationToken token)
        {
            if (survey == null)
            {
                logger.LogError("Failed to update survey. Survey is null");
                throw new ArgumentException("Failed to update survey. Survey is null");
            }

            if (survey.Id == default || string.IsNullOrEmpty(survey.Name) ||
                survey.CreatedDate == default || string.IsNullOrEmpty(survey.Title))
            {
                logger.LogError("Failed to update survey. Missing required fields");
                throw new ArgumentException("Failed to update survey. Missing required fields");
            }

            await mediator.Send(new UpdateSurveyCommand(survey), token);
        }

        public async Task<Guid> AddSurveyAsync(Survey survey, CancellationToken token)
        {
            if (survey == null)
            {
                logger.LogError("Failed to add survey. Survey is null");
                throw new ArgumentException("Failed to add survey. Survey is null");
            }

            if (string.IsNullOrEmpty(survey.Name) || survey.CreatedDate == default
                || string.IsNullOrEmpty(survey.Title))
            {
                logger.LogError("Failed to add survey. Missing required fields");
                throw new ArgumentException("Failed to add survey. Missing required fields");
            }

            return await mediator.Send(new AddSurveyCommand(survey), token);
        }

        public async Task<bool> DeleteSurveyAsync(Guid id, CancellationToken token)
        {
            if (id == default)
            {
                logger.LogError("Failed to delete survey. Wrong id");
                throw new ArgumentException("Failed to delete survey. Wrong id");
            }

            return await mediator.Send(new DeleteSurveyCommand(id), token);
            
        }

        public async Task<Survey> GetSurveyAsync(Guid id, CancellationToken token)
        {
            Survey survey = await mediator.Send(new GetSurveyByIdQuery(id), token);

            if (survey != null)
            {
                return survey;
            }

            logger.LogError("Failed to get survey. Wrong id");
            throw new ArgumentException("Failed to get survey. Wrong id");
        }

        public async Task<IEnumerable<Survey>> GetSurveysAsync(CancellationToken token)
        {
            var surveys = await mediator.Send(new GetSurveysQuery(), token);

            if (surveys != null)
            {
                return surveys;
            }

            logger.LogError("Failed to get surveys");
            throw new ArgumentException("Failed to get surveys");
        }

        public async Task<IEnumerable<Survey>> GetSurveysByUserIdAsync(Guid id, CancellationToken token)
        {
            var surveys = await mediator.Send(new GetSurveysByUserIdQuery(id), token);

            if (surveys != null)
            {
                return surveys;
            }

            logger.LogError("Failed to get surveys");
            throw new ArgumentException("Failed to get surveys");
        }
    }
}