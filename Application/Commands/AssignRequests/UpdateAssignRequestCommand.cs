using System;
using Domain.Entities.Auth;
using MediatR;

namespace Application.Commands.AssignRequests
{
    public record UpdateAssignRequestCommand(AssignRequest Request) : IRequest<Unit>;
}