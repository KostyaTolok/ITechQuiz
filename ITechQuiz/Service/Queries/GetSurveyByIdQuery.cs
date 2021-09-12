using ITechQuiz.Models;
using MediatR;
using System;

namespace ITechQuiz.Service.Queries
{
    public record GetSurveyByIdQuery(Guid Id) : IRequest<Survey>;
}
