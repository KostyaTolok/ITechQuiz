using System.Collections.Generic;
using Domain.Entities.Surveys;
using MediatR;

namespace Application.Commands.Answers
{
    public record AddAnswersCommand(IEnumerable<Answer> Answers) : IRequest<Unit>;
}