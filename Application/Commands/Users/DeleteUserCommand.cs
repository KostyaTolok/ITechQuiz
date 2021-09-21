using MediatR;
using Microsoft.AspNetCore.Identity;
using System;

namespace Application.Commands.Users
{
    public record DeleteUserCommand(Guid Id) : IRequest<IdentityResult>;

}
