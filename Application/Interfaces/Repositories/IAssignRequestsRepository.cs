using Domain.Entities.Auth;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IAssignRequestsRepository
    {
        Task<IEnumerable<AssignRequest>> GetAssignRequestsAsync(bool includeRejected,
            CancellationToken token);
        
        Task<IEnumerable<AssignRequest>> GetAssignRequestsSortedByDateAsync(bool includeRejected,
        CancellationToken token);
        
        Task<AssignRequest> GetAssignRequestAsync(Guid? id,CancellationToken token);

        Task AddAssignRequestAsync(AssignRequest request, CancellationToken token);

        Task UpdateAssignRequestAsync(AssignRequest request, CancellationToken token);

        Task DeleteAssignRequestAsync(AssignRequest request, CancellationToken token);
        
    }
}
