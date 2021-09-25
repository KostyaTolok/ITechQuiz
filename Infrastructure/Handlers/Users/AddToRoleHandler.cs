using Application.Commands.Users;
using Domain.Entities.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Handlers.Users
{
    public class AddToRoleHandler : IRequestHandler<AddToRoleCommand, IdentityResult>
    {
        private readonly UserManager<User> userManager;

        public AddToRoleHandler(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<IdentityResult> Handle(AddToRoleCommand request, CancellationToken token)
        {
            var user = await userManager.Users.FirstOrDefaultAsync(user => user.Id == request.Id, token);
            return await userManager.AddToRoleAsync(user, request.Role);
        }
    }
}
