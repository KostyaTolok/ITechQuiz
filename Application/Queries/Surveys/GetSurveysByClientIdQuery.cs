using System;
using System.Collections.Generic;
using Domain.Entities.Surveys;
using MediatR;

namespace Application.Queries.Surveys;

public record GetSurveysByClientIdQuery(Guid ClientId) : IRequest<IEnumerable<Survey>>;