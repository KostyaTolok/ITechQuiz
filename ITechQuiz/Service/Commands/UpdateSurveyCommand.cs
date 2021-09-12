using ITechQuiz.Models;
using MediatR;

namespace ITechQuiz.Service.Commands
{
    public record UpdateSurveyCommand(Survey Survey) : IRequest<Unit>;
}
