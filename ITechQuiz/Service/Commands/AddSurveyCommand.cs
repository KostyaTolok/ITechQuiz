using ITechQuiz.Models;
using MediatR;

namespace ITechQuiz.Service.Commands
{
    public record AddSurveyCommand(Survey Survey) : IRequest<Unit>;
}
