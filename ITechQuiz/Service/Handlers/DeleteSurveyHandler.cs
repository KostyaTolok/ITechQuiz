using ITechQuiz.Data.Interfaces;
using ITechQuiz.Service.Commands;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ITechQuiz.Service.Handlers
{
    public class DeleteSurveyHandler : IRequestHandler<DeleteSurveyCommand, Unit>
    {
        private readonly ISurveysRepository surveysRepository;

        public DeleteSurveyHandler(ISurveysRepository surveysRepository)
        {
            this.surveysRepository = surveysRepository;
        }

        public async Task<Unit> Handle(DeleteSurveyCommand command, CancellationToken token)
        {
            await surveysRepository.DeleteSurveyAsync(command.Id, token);
            return Unit.Value;
        }
    }
}
