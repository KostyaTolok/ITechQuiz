using System.Collections;
using System.Collections.Generic;
using Domain.Entities.Surveys;
using MediatR;

namespace Application.Commands.Surveys
{
    public record UpdateSurveyCommand(Survey Survey) : IRequest<Unit>;
}
