using ITechQuiz.Models;
using MediatR;
using System.Collections.Generic;

namespace ITechQuiz.Services.SurveyServices.Queries
{
    public record GetSurveysQuery() : IRequest<IEnumerable<Survey>>;
}
