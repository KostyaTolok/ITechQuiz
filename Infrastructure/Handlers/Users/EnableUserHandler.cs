using Application.Commands.Users;
using Domain.Entities.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Handlers.Users
{
    public class EnableUserHandler : IRequestHandler<EnableUserCommand, bool>
    {
        private readonly UserManager<User> userManager;

        public EnableUserHandler(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<bool> Handle(EnableUserCommand request, CancellationToken token)
        {
            var user = await userManager.Users.FirstOrDefaultAsync(user => user.Id == request.Id, token);
            if (user == null)
            {
                return false;
            }
            user.IsDisabled = false;
            await userManager.UpdateAsync(user);

            return true;
        }
    }
}
