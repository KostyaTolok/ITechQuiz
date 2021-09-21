using Domain.Entities.Surveys;
using MediatR;
using System;
using System.Collections.Generic;

namespace Application.Queries.Surveys
{
    public record GetSurveysByUserIdQuery(Guid Id) : IRequest<IEnumerable<Survey>>;
}
