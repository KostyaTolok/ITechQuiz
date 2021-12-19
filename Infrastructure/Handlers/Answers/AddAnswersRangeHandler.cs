using System.Threading;
using System.Threading.Tasks;
using Application.Commands.Answers;
using Application.Interfaces.Repositories;
using MediatR;

namespace Infrastructure.Handlers.Answers
{
    public class AddAnswersRangeHandler: IRequestHandler<AddAnswersCommand, Unit>
    {
        private readonly IAnswersRepository repository;

        public AddAnswersRangeHandler(IAnswersRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Unit> Handle(AddAnswersCommand request, CancellationToken token)
        {
            await repository.AddAnswersRangeAsync(request.Answers, token);
            return Unit.Value;
        }
    }
}