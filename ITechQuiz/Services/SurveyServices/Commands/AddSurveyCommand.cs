using ITechQuiz.Models;
using MediatR;

namespace ITechQuiz.Services.SurveyServices.Commands
{
    public record AddSurveyCommand(Survey Survey) : IRequest<Unit>;
}
