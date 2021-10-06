using Domain.Entities.Surveys;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IQuestionsRepository
    {
        Task<IEnumerable<Question>> GetQuestionsAsync();

        Task<Question> GetQuestionAsync(Guid id);

        Task AddQuestionAsync(Question question);

        Task UpdateQuestionAsync(Question question);

        Task DeleteQuestionAsync(Guid id);

    }
}
