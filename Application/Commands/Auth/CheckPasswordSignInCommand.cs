using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Commands.Auth
{
    public record CheckPasswordSignInCommand(string Email, string Password, bool RememberMe) : IRequest<SignInResult>;
}
