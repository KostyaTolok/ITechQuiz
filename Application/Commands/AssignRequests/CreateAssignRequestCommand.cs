using System;
using Domain.Entities.Auth;
using MediatR;

namespace Application.Commands.AssignRequests
{
    public record CreateAssignRequestCommand(AssignRequest AssignRequest) : IRequest<Guid>;

}
