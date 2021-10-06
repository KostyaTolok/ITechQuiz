using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using Application.Queries.AssignRequests;
using Domain.Entities.Auth;
using MediatR;

namespace Infrastructure.Handlers.AssignRequests
{

    public class GetAssignRequestsHandler : IRequestHandler<GetAssignRequestsQuery, IEnumerable<AssignRequest>>
    {
        private readonly IAssignRequestsRepository repository;

        public GetAssignRequestsHandler(IAssignRequestsRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<AssignRequest>> Handle(GetAssignRequestsQuery request, CancellationToken token)
        {
            return await repository.GetAssignRequestsAsync(request.IncludeRejected, token);
        }
    }
}
