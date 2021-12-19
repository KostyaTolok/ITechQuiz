using System;
using Domain.Entities.Surveys;
using MediatR;
using System.Collections.Generic;
using Domain.Enums;

namespace Application.Queries.Surveys
{
    public record GetSurveysQuery() :
        IRequest<IEnumerable<Survey>>;
}
