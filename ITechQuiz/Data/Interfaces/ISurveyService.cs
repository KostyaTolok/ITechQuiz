using ITechQuiz.Models;
using ITechQuiz.Service.Commands;
using ITechQuiz.Service.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ITechQuiz.Data.Interfaces
{
    public interface ISurveyService
    {
        Task<IEnumerable<Survey>> GetSurveysAsync(CancellationToken token);

        Task<Survey> GetSurveyAsync(Guid id, CancellationToken token);

        Task AddSurveyAsync(Survey survey, CancellationToken token);

        Task UpdateSurveyAsync(Survey survey, CancellationToken token);

        Task DeleteSurveyAsync(Guid id, CancellationToken token);
    }
}
