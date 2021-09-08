using ITechQuiz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITechQuiz.Data.Interfaces
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
