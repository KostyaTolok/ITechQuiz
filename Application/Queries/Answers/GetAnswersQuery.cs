using System;
using System.Collections.Generic;
using Domain.Entities.Surveys;
using MediatR;

namespace Application.Queries.Answers
{
    public record GetAnswersQuery(Guid? SurveyId, Guid? UserId): IRequest<IEnumerable<Answer>>;
}