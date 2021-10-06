using System.Collections.Generic;
using Domain.Entities.Auth;
using MediatR;

namespace Application.Queries.AssignRequests
{
    public record GetAssignRequestsSortedByDate(bool IncludeRejected) :IRequest<IEnumerable<AssignRequest>>;
}