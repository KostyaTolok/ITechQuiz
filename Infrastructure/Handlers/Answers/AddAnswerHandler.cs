using System.Reflection.Metadata.Ecma335;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.Answers;
using Application.Interfaces.Repositories;
using MediatR;

namespace Infrastructure.Handlers.Answers
{
    public class AddAnswerHandler: IRequestHandler<AddAnswerCommand, Unit>
    {
        private readonly IAnswersRepository repository;

        public AddAnswerHandler(IAnswersRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Unit> Handle(AddAnswerCommand command, CancellationToken token)
        {
            await repository.AddAnswerAsync(command.Answer, token);
            return Unit.Value;
        }
    }
}