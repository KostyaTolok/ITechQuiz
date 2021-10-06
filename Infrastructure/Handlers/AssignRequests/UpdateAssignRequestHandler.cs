using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.AssignRequests;
using Application.Interfaces.Repositories;
using Application.Queries.AssignRequests;
using Domain.Entities.Auth;
using MediatR;

namespace Infrastructure.Handlers.AssignRequests
{
    public class UpdateAssignRequestHandler: IRequestHandler<UpdateAssignRequestCommand, Unit>
    {
        private readonly IAssignRequestsRepository repository;

        public UpdateAssignRequestHandler(IAssignRequestsRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Unit> Handle(UpdateAssignRequestCommand command, CancellationToken token)
        {
            await repository.UpdateAssignRequestAsync(command.Request, token);
            return Unit.Value;
        }
    }
}