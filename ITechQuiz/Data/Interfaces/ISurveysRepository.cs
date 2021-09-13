using ITechQuiz.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ITechQuiz.Data.Interfaces
{
    public interface ISurveysRepository
    {
        Task<IEnumerable<Survey>> GetSurveysAsync(CancellationToken token);

        Task<IEnumerable<Survey>> GetSurveysByUserIdAsync(Guid id, CancellationToken token);

        Task<Survey> GetSurveyAsync(Guid id, CancellationToken token);

        Task AddSurveyAsync(Survey survey, CancellationToken token);

        Task UpdateSurveyAsync(Survey survey, CancellationToken token);

        Task DeleteSurveyAsync(Guid id, CancellationToken token);
    }
}
