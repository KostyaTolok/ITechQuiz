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

        public async Task<bool> Handle(DisableUserCommand request, CancellationToken token)
        {
            var user = await userManager.Users.FirstOrDefaultAsync(user => user.Id == request.Id, token);
            if (user == null)
            {
                return false;
            }
            user.IsDisabled = true;
            user.DisabledEnd = request.EndDate;
            await userManager.UpdateAsync(user);

            return true;
        }
    }
}
