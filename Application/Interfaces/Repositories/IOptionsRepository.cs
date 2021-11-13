using Domain.Entities.Surveys;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IOptionsRepository
    {
        Task<IEnumerable<Option>> GetOptionsAsync(CancellationToken token);

        Task<Option> GetOptionAsync(Guid id, CancellationToken token);

        Task AddOptionAsync(Option option, CancellationToken token);

        Task UpdateOptionAsync(Option option, CancellationToken token);

        Task DeleteOptionAsync(Option option, CancellationToken token);
    }
}
