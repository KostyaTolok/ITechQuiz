using System.Collections.Generic;
using Domain.Entities.Surveys;
using MediatR;

namespace Application.Commands.Answers
{
    public record AddAnswerCommand(Answer Answer) : IRequest<Unit>;
}