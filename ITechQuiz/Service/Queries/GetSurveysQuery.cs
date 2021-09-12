using ITechQuiz.Models;
using MediatR;
using System.Collections.Generic;

namespace ITechQuiz.Service.Queries
{
    public record GetSurveysQuery() : IRequest<IEnumerable<Survey>>;
}
