using System;
using System.Collections.Generic;
using Domain.Entities.Surveys;
using MediatR;

namespace Application.Queries.Surveys
{
    public record GetSurveysByUserIdQuery(Guid UserId, ICollection<Category> Categories,
        bool SortedByDate) : IRequest<IEnumerable<Survey>>;
}