using Domain.Entities.Surveys;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IOptionsRepository
    {
        Task<IEnumerable<Option>> GetOptionsAsync();

        Task<Option> GetOptionAsync(Guid id);

        Task AddOptionAsync(Option option);

        Task UpdateOptionAsync(Option option);

        Task DeleteOptionAsync(Guid id);
    }
}
