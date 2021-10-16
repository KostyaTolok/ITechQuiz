using Application.DTO;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Enums;

namespace Application.Interfaces.Services
{
    public interface ISurveysService
    {
        Task<IEnumerable<SurveyDTO>> GetSurveysAsync(Guid? userId,
            string type, CancellationToken token);

        Task<SurveyDTO> GetSurveyAsync(Guid id, CancellationToken token);

        Task<Guid> AddSurveyAsync(SurveyDTO survey, CancellationToken token);

        Task UpdateSurveyAsync(SurveyDTO survey, CancellationToken token);

        Task<bool> DeleteSurveyAsync(Guid id, CancellationToken token);
    }
}
