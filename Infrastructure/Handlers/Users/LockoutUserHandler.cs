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
    public class LockoutUserHandler : IRequestHandler<LockoutUserCommand, IdentityResult>
    {
        private readonly UserManager<User> userManager;

        public LockoutUserHandler(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<IdentityResult> Handle(LockoutUserCommand request, CancellationToken token)
        {
            var user = await userManager.Users.FirstOrDefaultAsync(user => user.Id == request.Id, token);
            var setResult = await userManager.SetLockoutEnabledAsync(user, true);
            var dateResult = await userManager.SetLockoutEndDateAsync(user, request.EndDate);
            if (setResult.Succeeded && dateResult.Succeeded)
            {
                return IdentityResult.Success;
            }
            else
            {
                return IdentityResult.Failed();
            }
        }
    }
}
