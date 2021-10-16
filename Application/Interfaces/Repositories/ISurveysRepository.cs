using Domain.Entities.Surveys;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Enums;

namespace Application.Interfaces.Repositories
{
    public interface ISurveysRepository
    {
        Task<IEnumerable<Survey>> GetSurveysAsync(Guid? id, SurveyTypes? type,
            CancellationToken token);
        
        Task<Survey> GetSurveyAsync(Guid id, CancellationToken token);

        Task AddSurveyAsync(Survey survey, CancellationToken token);

        Task UpdateSurveyAsync(Survey survey, CancellationToken token);

        Task DeleteSurveyAsync(Survey survey, CancellationToken token);
    }
}
