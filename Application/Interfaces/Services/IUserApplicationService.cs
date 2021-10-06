using Application.DTO;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IAssignRequestsService
    {
        Task<IEnumerable<AssignRequestDTO>> GetAssignRequestsAsync(bool includeRejected,
            bool sorted, CancellationToken token);

        Task<bool> DeleteAssignRequestAsync(Guid id, CancellationToken token);

        Task<Guid> CreateAssignRequestAsync(CreateAssignRequestModel model, CancellationToken token);

        Task<bool> AcceptAssignRequestAsync(Guid id, CancellationToken token);
        
        Task RejectAssignRequestAsync(Guid id, CancellationToken token);

    }
}
