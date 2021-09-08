using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITechQuiz.Data.Interfaces;
using ITechQuiz.Models;
using Microsoft.Extensions.Logging;

namespace ITechQuiz.Data.Services
{
    public class SurveyService : ISurveyService
    {
        private readonly ISurveysRepository surveysRepository;
        private readonly ILogger<SurveyService> logger;

        public SurveyService(ISurveysRepository surveysRepository, ILogger<SurveyService> logger)
        {
            this.surveysRepository = surveysRepository;
            this.logger = logger;
        }

        public async Task UpdateSurveyAsync(Survey survey)
        {
            if (survey == null)
            {
                logger.LogError("Failed to update survey. Survey is null");
                throw new ArgumentException("Failed to update survey. Survey is null");
            }

            if (string.IsNullOrEmpty(survey.Name) || survey.CreatedDate == default || string.IsNullOrEmpty(survey.Title))
            {
                logger.LogError("Failed to update survey. Missing required fields");
                throw new ArgumentException("Failed to update survey. Missing required fields");
            }

            await surveysRepository.UpdateSurveyAsync(survey);
        }

        public async Task AddSurveyAsync(Survey survey)
        {
            if (survey == null)
            {
                logger.LogError("Failed to add survey. Survey is null");
                throw new ArgumentException("Failed to add survey. Survey is null");
            }

            if (string.IsNullOrEmpty(survey.Name) || survey.CreatedDate == default || string.IsNullOrEmpty(survey.Title))
            {
                logger.LogError("Failed to add survey. Missing required fields");
                throw new ArgumentException("Failed to add survey. Missing required fields");
            }

            await surveysRepository.AddSurveyAsync(survey);
        }

        public async Task DeleteSurveyAsync(string Name)
        {
            if (string.IsNullOrEmpty(Name))
            {
                logger.LogError("Failed to delete survey. Empty name");
                throw new ArgumentException("Failed to delete survey. Empty name");
            }
            await surveysRepository.DeleteSurveyAsync(Name);
        }

        public async Task<Survey> GetSurveyAsync(string Name)
        {
            var survey = await surveysRepository.GetSurveyAsync(Name);

            if (survey != null)
            {
                return survey;
            }

            logger.LogError("Failed to delete survey. Wrong name");
            throw new ArgumentException("Failed to get survey. Wrong name");
        }

        public async Task<IEnumerable<Survey>> GetSurveysAsync()
        {
            var surveys = await surveysRepository.GetSurveysAsync();

            if (surveys != null)
            {
                return surveys;
            }

            logger.LogError("Failed to get surveys");
            throw new ArgumentException("Failed to get surveys");
        }
    }
}
