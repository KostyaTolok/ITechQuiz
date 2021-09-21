using Domain.Entities.Surveys;
using MediatR;
using System;

namespace Application.Queries.Surveys
{
    public record GetSurveyByIdQuery(Guid Id) : IRequest<Survey>;
}
