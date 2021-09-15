﻿using ITechQuiz.Data.Interfaces;
using ITechQuiz.Models;
using ITechQuiz.Services.SurveyServices.Commands;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ITechQuiz.Services.SurveyServices.Handlers
{
    public class AddSurveyHandler : IRequestHandler<AddSurveyCommand, Unit>
    {
        private readonly ISurveysRepository surveysRepository;

        public AddSurveyHandler(ISurveysRepository surveysRepository)
        {
            this.surveysRepository = surveysRepository;
        }

        public async Task<Unit> Handle(AddSurveyCommand command, CancellationToken token)
        {
            Survey survey = command.Survey;

            await surveysRepository.AddSurveyAsync(survey, token);
            return Unit.Value;
        }
    }
}