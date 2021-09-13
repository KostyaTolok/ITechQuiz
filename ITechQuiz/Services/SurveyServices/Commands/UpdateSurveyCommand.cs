using ITechQuiz.Models;
using MediatR;

namespace ITechQuiz.Services.SurveyServices.Commands
{
    public record UpdateSurveyCommand(Survey Survey) : IRequest<Unit>;
}
