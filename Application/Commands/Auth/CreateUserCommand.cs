using Domain.Entities.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Commands.Auth
{
    public record CreateUserCommand(User User, string Password) : IRequest<IdentityResult>;
}
