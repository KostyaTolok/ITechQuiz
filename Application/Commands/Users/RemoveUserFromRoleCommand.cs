using System;
using MediatR;

namespace Application.Commands.Users
{
    public record RemoveUserFromRoleCommand(Guid Id, string Role) : IRequest<bool>;
}