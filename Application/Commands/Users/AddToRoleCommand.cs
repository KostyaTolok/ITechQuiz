using Domain.Enums;
using MediatR;
using System;

namespace Application.Commands.Users
{
    public record AddToRoleCommand(Guid Id, Roles Role) : IRequest<bool>;
}
