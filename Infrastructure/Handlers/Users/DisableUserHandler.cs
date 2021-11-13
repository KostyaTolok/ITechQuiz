using Application.Commands.Users;
using Domain.Entities.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Handlers.Users
{
    public class DisableUserHandler : IRequestHandler<DisableUserCommand, bool>
    {
        private readonly UserManager<User> userManager;

        public DisableUserHandler(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<bool> Handle(DisableUserCommand command, CancellationToken token)
        {
            var user = await userManager.Users.FirstOrDefaultAsync(user => user.Id == command.Id, token);
            if (user == null)
            {
                return false;
            }
            user.IsDisabled = true;
            
            user.DisabledEnd = command.EndDate ?? DateTime.Now.AddDays(7);
            
            await userManager.UpdateAsync(user);

            return true;
        }
    }
}
