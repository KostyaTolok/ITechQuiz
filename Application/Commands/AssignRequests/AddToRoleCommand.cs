using System;
using Domain.Enums;
using MediatR;

namespace Application.Commands.AssignRequests
{
    public record AddToRoleCommand(Guid Id, Roles Role) : IRequest<bool>;
}
