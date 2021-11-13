using System.Threading;
using System.Threading.Tasks;
using Application.Commands.Auth;
using Domain.Entities.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Handlers.Auth
{
    public class ChangePasswordHandler : IRequestHandler<ChangePasswordCommand, IdentityResult>
    {
        private readonly UserManager<User> userManager;

        public ChangePasswordHandler(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<IdentityResult> Handle(ChangePasswordCommand command, CancellationToken token)
        {
            return await userManager.ChangePasswordAsync(command.User,
                command.OldPassword, command.NewPassword);
        }
    }
}