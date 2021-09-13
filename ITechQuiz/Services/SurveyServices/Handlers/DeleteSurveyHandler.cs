using ITechQuiz.Data.Interfaces;
using ITechQuiz.Services.SurveyServices.Commands;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ITechQuiz.Services.SurveyServices.Handlers
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
