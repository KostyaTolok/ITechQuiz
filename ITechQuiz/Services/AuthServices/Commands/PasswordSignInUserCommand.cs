using ITechQuiz.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ITechQuiz.Services.AuthServices.Commands
{
    public record PasswordSignInUserCommand(string Email, string Password, bool RememberMe) : IRequest<SignInResult>;
}
