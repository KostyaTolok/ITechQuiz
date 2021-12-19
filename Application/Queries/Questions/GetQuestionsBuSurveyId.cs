using System;
using System.Collections;
using System.Collections.Generic;
using Domain.Entities.Surveys;
using MediatR;

namespace Application.Queries.Questions
{
    public record GetQuestionsBySurveyId(Guid SurveyId) : IRequest<IEnumerable<Question>>;
}