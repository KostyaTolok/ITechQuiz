using ITechQuiz.Data.Interfaces;
using ITechQuiz.Models;
using ITechQuiz.Service.Commands;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ITechQuiz.Service.Handlers
{
    public class UpdateSurveyHandler : IRequestHandler<UpdateSurveyCommand, Unit>
    {
        private readonly ISurveysRepository surveysRepository;

        public UpdateSurveyHandler(ISurveysRepository surveysRepository)
        {
            this.surveysRepository = surveysRepository;
        }

        public async Task<Unit> Handle(UpdateSurveyCommand command, CancellationToken token)
        {
            Survey survey = command.Survey;

            await surveysRepository.UpdateSurveyAsync(survey, token);
            return Unit.Value;
        }
    }
}
