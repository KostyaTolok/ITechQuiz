using Domain.Entities.Surveys;
using MediatR;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Application.Commands.Surveys
{
    public record AddSurveyCommand(Survey Survey) : IRequest<Guid>;
}
