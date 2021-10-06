using System;
using Domain.Entities.Auth;
using MediatR;

namespace Application.Queries.AssignRequests
{
    public record GetAssignRequestByIdQuery(Guid Id) : IRequest<AssignRequest>;
}
