using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using Application.Queries.AssignRequests;
using Domain.Entities.Auth;
using MediatR;

namespace Infrastructure.Handlers.AssignRequests
{
    public class GetAssignRequestByIdHandler : IRequestHandler<GetAssignRequestByIdQuery, AssignRequest>
    {
        private readonly IAssignRequestsRepository repository;

        public GetAssignRequestByIdHandler(IAssignRequestsRepository repository)
        {
            this.repository = repository;
        }

        public async Task<AssignRequest> Handle(GetAssignRequestByIdQuery request, CancellationToken token)
        {
            return await repository.GetAssignRequestAsync(request.Id, token);
        }
    }
}
