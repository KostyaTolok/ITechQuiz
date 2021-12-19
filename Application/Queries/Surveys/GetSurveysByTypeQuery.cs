using System;
using System.Collections.Generic;
using Domain.Entities.Surveys;
using Domain.Enums;
using MediatR;

namespace Application.Queries.Surveys
{
    public record GetSurveysByTypeQuery(SurveyTypes Type, ICollection<Category> Categories) : IRequest<IEnumerable<Survey>>;
}