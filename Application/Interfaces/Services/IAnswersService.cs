using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.DTO;
using Domain.Entities.Surveys;
using Domain.Models;

namespace Application.Interfaces.Services
{
    public interface IAnswersService
    {
        Task AddAnswersAsync(IEnumerable<AnswerDTO> models, Guid? userId, CancellationToken token);

        Task<IEnumerable<AnswerDTO>> GetAnswersAsync(Guid? surveyId, Guid? userId, CancellationToken token);
    }
}