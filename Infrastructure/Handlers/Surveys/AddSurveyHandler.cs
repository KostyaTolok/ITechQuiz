using Application.Commands.Surveys;
using Application.Interfaces.Data;
using Domain.Entities.Surveys;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Handlers.Surveys
{
    public class AddSurveyHandler : IRequestHandler<AddSurveyCommand, Guid>
    {
        private readonly ISurveysRepository surveysRepository;

        public AddSurveyHandler(ISurveysRepository surveysRepository)
        {
            this.surveysRepository = surveysRepository;
        }

        public async Task<Guid> Handle(AddSurveyCommand command, CancellationToken token)
        {
            Survey survey = command.Survey;

            await surveysRepository.AddSurveyAsync(survey, token);
            return survey.Id;
        }
    }
}
