using ITechQuiz.Models;
using MediatR;
using System;

namespace ITechQuiz.Services.SurveyServices.Commands
{
    public record AddSurveyCommand(Survey Survey) : IRequest<Guid>;
}
