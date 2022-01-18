using System;
using System.Collections;
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
using Application.Queries.Categories;
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

                foreach (var option in question.Options)
                {
                    if (string.IsNullOrEmpty(option.Title))
                    {
                        logger.LogError(SurveyServiceStrings.UpdateSurveyOptionTitleException);
                        throw new ArgumentException(SurveyServiceStrings.UpdateSurveyOptionTitleException);
                    }
                }
            }

            Survey oldSurvey;
            try
            {
                oldSurvey = await mediator.Send(new GetSurveyByIdQuery(survey.Id), token);
            }
            catch (Exception)
            {
                logger.LogError(SurveyServiceStrings.UpdateSurveyException);
                throw new ArgumentException(SurveyServiceStrings.UpdateSurveyException);
            }

            if (oldSurvey.Equals(survey))
            {
                logger.LogError(SurveyServiceStrings.UpdateSurveyNoChangesException);
                throw new ArgumentException(SurveyServiceStrings.UpdateSurveyNoChangesException);
            }

            try
            {
                survey.UpdatedDate = DateTime.Now;
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
            Guid? userId, CancellationToken token)
        {
            if (surveyDTO == null)
            {
                logger.LogError(SurveyServiceStrings.AddSurveyNullException);
                throw new ArgumentException(SurveyServiceStrings.AddSurveyNullException);
            }

            if (!userId.HasValue)
            {
                logger.LogError(SurveyServiceStrings.AddSurveyUserIdException);
                throw new ArgumentException(SurveyServiceStrings.AddSurveyUserIdException);
            }

            if (string.IsNullOrEmpty(surveyDTO.Title))
            {
                logger.LogError(SurveyServiceStrings.AddSurveyTitleException);
                throw new ArgumentException(SurveyServiceStrings.AddSurveyTitleException);
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
                survey.UpdatedDate = DateTime.Now;
                survey.UserId = userId.Value;
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
            bool client, string type, ICollection<Guid> categoryIds, bool sortedByDate, CancellationToken token)
        {
            IEnumerable<Survey> surveys;
            try
            {
                SurveyTypes? surveyType =
                    Enum.TryParse(type, true, out SurveyTypes result) ? result : null;

                var categories = new List<Category>();
                foreach (var id in categoryIds)
                {
                    var category = await mediator.Send(new GetCategoryQuery(id), token);

                    if (category == null)
                    {
                        logger.LogError
                            ("{ExString}", SurveyServiceStrings.GetSurveysException);
                        throw new Exception(SurveyServiceStrings.GetSurveysException);
                    }

                    categories.Add(category);
                }

                if (client && userId.HasValue)
                {
                    surveys = await mediator.Send(new GetSurveysByClientIdQuery(userId.Value), token);
                }
                else if (userId.HasValue)
                {
                    surveys = await mediator.Send(
                        new GetSurveysByUserIdQuery(userId.Value, categories, sortedByDate), token);
                }
                else if (surveyType.HasValue)
                {
                    surveys = await mediator.Send(new GetSurveysByTypeQuery(surveyType.Value, categories), token);
                }
                else
                    surveys = await mediator.Send(new GetSurveysQuery(), token);
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