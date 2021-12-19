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
        Task<IEnumerable<Survey>> GetSurveysAsync(CancellationToken token);
        

        Task<IEnumerable<Survey>> GetSurveysByTypeAsync(SurveyTypes type, ICollection<Category> categories,
            CancellationToken token);

        Task<IEnumerable<Survey>> GetSurveysByUserId(Guid userId, ICollection<Category> categories,
            CancellationToken token);
        
        Task<IEnumerable<Survey>> GetSurveysByClientId(Guid clientId,
            CancellationToken token);

        Task<Survey> GetSurveyAsync(Guid id, CancellationToken token);

        Task<Survey> GetSurveyByQuestionId(Guid questionId, CancellationToken token);

        Task AddSurveyAsync(Survey survey, CancellationToken token);

        Task UpdateSurveyAsync(Survey survey, CancellationToken token);

        Task DeleteSurveyAsync(Survey survey, CancellationToken token);
    }
}
