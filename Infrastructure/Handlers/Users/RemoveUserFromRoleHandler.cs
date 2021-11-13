using System.Threading;
using System.Threading.Tasks;
using Application.Commands.Users;
using Domain.Entities.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Handlers.Users
{
    public class RemoveUserFromRoleHandler: IRequestHandler<RemoveUserFromRoleCommand, bool>
    {
        private readonly UserManager<User> userManager;

        public RemoveUserFromRoleHandler(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<bool> Handle(RemoveUserFromRoleCommand request, CancellationToken token)
        {
            var user = await userManager.Users.FirstOrDefaultAsync(user => user.Id == request.Id, token);
            if (user == null)
            {
                return false;
            }
            await userManager.RemoveFromRoleAsync(user, request.Role.ToLower());
            return true;
        }
    }
}