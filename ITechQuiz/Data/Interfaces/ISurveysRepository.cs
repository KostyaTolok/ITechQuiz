using ITechQuiz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITechQuiz.Data.Interfaces
{
    interface ISurveysRepository
    {
        Task<IEnumerable<Survey>> GetSurveysAsync();

        Task<Survey> GetSurveyAsync(string Name);

        Task AddSurveyAsync(Survey survey);

        Task UpdateSurveyAsync(Survey survey);

        Task DeleteSurveyAsync(string Name);
    }
}
