using ITechQuiz.Data.Interfaces;
using ITechQuiz.Models;
using ITechQuiz.Services.SurveyServices.Commands;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ITechQuiz.Services.SurveyServices.Handlers
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
