using System.Threading;
using System.Threading.Tasks;
using Application.Commands.AssignRequests;
using Application.Interfaces.Repositories;
using MediatR;

namespace Infrastructure.Handlers.AssignRequests
{
    public class DeleteAssignRequestHandler : IRequestHandler<DeleteAssignRequestCommand, bool>
    {
        private readonly IAssignRequestsRepository assignRequestsRepository;

        public DeleteAssignRequestHandler(IAssignRequestsRepository assignRequestsRepository)
        {
            this.assignRequestsRepository = assignRequestsRepository;
        }

        public async Task<bool> Handle(DeleteAssignRequestCommand command, CancellationToken token)
        {
            var assign = await assignRequestsRepository.GetAssignRequestAsync(command.Id, token);
            if (assign == null)
            {
                return false;
            }
            await assignRequestsRepository.DeleteAssignRequestAsync(assign, token);
            return true;
        }
    }
}
