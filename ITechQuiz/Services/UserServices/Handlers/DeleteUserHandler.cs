﻿using ITechQuiz.Models;
using ITechQuiz.Services.UserServices.Commands;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace ITechQuiz.Services.UserServices.Handlers
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, IdentityResult>
    {
        private readonly UserManager<User> userManager;

        public DeleteUserHandler(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<IdentityResult> Handle(DeleteUserCommand request, CancellationToken token)
        {
            var user = await userManager.Users.FirstOrDefaultAsync(user => user.Id == request.Id, token);
            return await userManager.DeleteAsync(user);
        }
    }
}
