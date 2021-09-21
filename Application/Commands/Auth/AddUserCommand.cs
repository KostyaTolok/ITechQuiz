using Domain.Entities.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Commands.Auth
{
    public record AddUserCommand(User User, string Password) : IRequest<IdentityResult>;
}
