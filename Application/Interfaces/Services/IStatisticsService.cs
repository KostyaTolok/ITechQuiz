using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.DTO;

namespace Application.Interfaces.Services
{
    public interface IStatisticsService
    {
        Task<IEnumerable<QuestionStatisticsDTO>> GetStatisticsAsync(Guid surveyId,
            CancellationToken token);
    }
}