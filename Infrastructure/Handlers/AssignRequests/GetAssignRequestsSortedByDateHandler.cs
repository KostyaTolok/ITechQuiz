using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using Application.Queries.AssignRequests;
using Domain.Entities.Auth;
using MediatR;

namespace Infrastructure.Handlers.AssignRequests
{
    public class
        GetAssignRequestsSortedByDateHandler : IRequestHandler<GetAssignRequestsSortedByDate,
            IEnumerable<AssignRequest>>
    {
        private readonly IAssignRequestsRepository repository;

        public GetAssignRequestsSortedByDateHandler(IAssignRequestsRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<AssignRequest>> Handle(GetAssignRequestsSortedByDate request,
            CancellationToken token)
        {
            return await repository.GetAssignRequestsSortedByDateAsync(
                request.IncludeRejected, token);
        }
    }
}