using Application.Commands.Surveys;
using Application.Interfaces.Data;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Handlers.Surveys
{
    public class DeleteSurveyHandler : IRequestHandler<DeleteSurveyCommand, bool>
    {
        private readonly ISurveysRepository surveysRepository;

        public DeleteSurveyHandler(ISurveysRepository surveysRepository)
        {
            this.surveysRepository = surveysRepository;
        }

        public async Task<bool> Handle(DeleteSurveyCommand command, CancellationToken token)
        {
            var survey = await surveysRepository.GetSurveyAsync(command.Id, token);
            if (survey == null)
            {
                return false;
            }
            await surveysRepository.DeleteSurveyAsync(survey, token);
            return true;
        }
    }
}
