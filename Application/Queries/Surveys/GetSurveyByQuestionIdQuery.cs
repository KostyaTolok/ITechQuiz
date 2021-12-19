using System;
using Domain.Entities.Surveys;
using MediatR;

namespace Application.Queries.Surveys
{
    public record GetSurveyByQuestionIdQuery(Guid QuestionId) : IRequest<Survey>;
}