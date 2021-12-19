using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities.Surveys;

namespace Application.Interfaces.Repositories
{
    public interface IAnswersRepository
    {
        Task AddAnswerAsync(Answer answer, CancellationToken token);
        
        Task DeleteAnswerAsync(Answer answer, CancellationToken token);

        Task AddAnswersRangeAsync(IEnumerable<Answer> answers, CancellationToken token);

        Task<IEnumerable<Answer>> GetAnswersAsync(Guid? surveyId,
            Guid? userId, CancellationToken token);
    }
}