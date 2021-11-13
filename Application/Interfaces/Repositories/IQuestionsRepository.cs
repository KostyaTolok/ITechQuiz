using Domain.Entities.Surveys;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IQuestionsRepository
    {
        Task<IEnumerable<Question>> GetQuestionsAsync(CancellationToken token);

        Task<Question> GetQuestionAsync(Guid id, CancellationToken token);

        Task AddQuestionAsync(Question question, CancellationToken token);

        Task UpdateQuestionAsync(Question question, CancellationToken token);

        Task DeleteQuestionAsync(Question question, CancellationToken token);

    }
}
