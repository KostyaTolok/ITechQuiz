using System.Collections.Generic;
using Domain.Entities.Auth;
using MediatR;

namespace Application.Queries.AssignRequests
{
    public record GetAssignRequestsQuery(bool IncludeRejected) : IRequest<IEnumerable<AssignRequest>>;
}
