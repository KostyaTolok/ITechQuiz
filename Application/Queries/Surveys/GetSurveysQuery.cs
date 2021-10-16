using System;
using Domain.Entities.Surveys;
using MediatR;
using System.Collections.Generic;
using Domain.Enums;

namespace Application.Queries.Surveys
{
    public record GetSurveysQuery(Guid? UserId, SurveyTypes? Type) :
        IRequest<IEnumerable<Survey>>;
}
