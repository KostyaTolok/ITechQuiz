using ITechQuiz.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace ITechQuiz.Services.SurveyServices.Queries
{
    public record GetSurveysByUserIdQuery(Guid Id) : IRequest<IEnumerable<Survey>>;
}
