using MediatR;
using System;

namespace ITechQuiz.Service.Commands
{
    public record DeleteSurveyCommand(Guid Id) : IRequest<Unit>;
}
