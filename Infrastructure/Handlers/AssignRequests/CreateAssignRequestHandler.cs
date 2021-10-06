using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.AssignRequests;
using Application.Interfaces.Repositories;
using Domain.Entities.Auth;
using MediatR;

namespace Infrastructure.Handlers.AssignRequests
{

    public class CreateAssignRequestHandler : IRequestHandler<CreateAssignRequestCommand, Guid>
    {
        private readonly IAssignRequestsRepository assignRequestsRepository;

        public CreateAssignRequestHandler(IAssignRequestsRepository assignRequestsRepository)
        {
            this.assignRequestsRepository = assignRequestsRepository;
        }

        public async Task<Guid> Handle(CreateAssignRequestCommand command, CancellationToken token)
        {
            var assign = command.AssignRequest;

            await assignRequestsRepository.AddAssignRequestAsync(assign, token);
            return assign.Id;
        }
    }
}
