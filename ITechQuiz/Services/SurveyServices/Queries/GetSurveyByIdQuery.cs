using ITechQuiz.Models;
using MediatR;
using System;

namespace ITechQuiz.Services.SurveyServices.Queries
{
    public record GetSurveyByIdQuery(Guid Id) : IRequest<Survey>;
}
