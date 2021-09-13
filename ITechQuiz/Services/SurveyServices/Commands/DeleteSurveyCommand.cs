using MediatR;
using System;

namespace ITechQuiz.Services.SurveyServices.Commands
{
    public record DeleteSurveyCommand(Guid Id) : IRequest<Unit>;
}
