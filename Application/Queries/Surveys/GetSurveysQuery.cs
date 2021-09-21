using Domain.Entities.Surveys;
using MediatR;
using System.Collections.Generic;

namespace Application.Queries.Surveys
{
    public record GetSurveysQuery() : IRequest<IEnumerable<Survey>>;
}
