using Application.Commands.Surveys;
using Application.Interfaces.Repositories;
using Domain.Entities.Surveys;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Handlers.Surveys
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
            await surveysRepository.UpdateSurveyAsync(command.Survey, token);
            return Unit.Value;
        }
    }
}