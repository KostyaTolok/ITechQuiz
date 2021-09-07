using ITechQuiz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITechQuiz.Data.Interfaces
{
    interface IOptionsRepository
    {
        Task<IEnumerable<Option>> GetOptionsAsync();

        Task<Option> GetOptionAsync(Guid id);

        Task AddOptionAsync(Option option);

        Task UpdateOptionAsync(Option option);

        Task DeleteOptionAsync(Guid id);
    }
}
