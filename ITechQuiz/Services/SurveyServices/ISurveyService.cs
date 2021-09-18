using ITechQuiz.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ITechQuiz.Services.SurveyServices
{
    public interface ISurveyService
    {
        Task<IEnumerable<Survey>> GetSurveysAsync(CancellationToken token);

        Task<IEnumerable<Survey>> GetSurveysByUserIdAsync(Guid id, CancellationToken token);

        Task<Survey> GetSurveyAsync(Guid id, CancellationToken token);

        Task<Guid> AddSurveyAsync(Survey survey, CancellationToken token);

        Task UpdateSurveyAsync(Survey survey, CancellationToken token);

        Task DeleteSurveyAsync(Guid id, CancellationToken token);
    }
}
