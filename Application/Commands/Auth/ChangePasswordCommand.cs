using Domain.Entities.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Commands.Auth
{
    public record ChangePasswordCommand(User User, string OldPassword,
        string NewPassword) : IRequest<IdentityResult>;
}