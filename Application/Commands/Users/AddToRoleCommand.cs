using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;

namespace Application.Commands.Users
{
    public record AddToRoleCommand(Guid Id, Roles Role) : IRequest<IdentityResult>;
}
