using Application.Interfaces.Repositories;
using Domain.Entities.Auth;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    [ExcludeFromCodeCoverage]
    public class EFAssignRequestsRepository : IAssignRequestsRepository
    {
        private readonly QuizDbContext context;

        public EFAssignRequestsRepository(QuizDbContext context)
        {
            this.context = context;
        }

        public async Task AddAssignRequestAsync(AssignRequest request, CancellationToken token)
        {
            await context.AssignRequests.AddAsync(request, token);
            await context.SaveChangesAsync(token);
        }

        public async Task UpdateAssignRequestAsync(AssignRequest request, CancellationToken token)
        {
            context.AssignRequests.Update(request);
            await context.SaveChangesAsync(token);
        }

        public async Task DeleteAssignRequestAsync(AssignRequest request, CancellationToken token)
        {
            context.AssignRequests.Remove(request);
            await context.SaveChangesAsync(token);
        }

        public async Task<IEnumerable<AssignRequest>> GetAssignRequestsSortedByDateAsync(
            bool includeRejected, CancellationToken token)
        {
            return await context.AssignRequests
                .Include(a => a.User)
                .Where(a => includeRejected ? true : a.IsRejected == false)
                .OrderBy(a => a.CreatedDate)
                .ToListAsync(token);
        }

        public async Task<AssignRequest> GetAssignRequestAsync(Guid? id, CancellationToken token)
        {
            return await context.AssignRequests
                .Include(a => a.User)
                .FirstOrDefaultAsync(a => a.Id == id, token);
        }

        public async Task<IEnumerable<AssignRequest>> GetAssignRequestsAsync(bool includeRejected,
            CancellationToken token)
        {
            return await context.AssignRequests
                .Include(a => a.User)
                .Where(a => includeRejected ? true : a.IsRejected == false)
                .ToListAsync(token);
        }
        
    }
}