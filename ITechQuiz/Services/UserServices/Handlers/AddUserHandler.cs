using ITechQuiz.Models;
using ITechQuiz.Services.UserServices.Commands;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace ITechQuiz.Services.UserServices.Handlers
{
    public class AddUserHandler : IRequestHandler<AddUserCommand, IdentityResult>
    {
        private readonly UserManager<User> userManager;

        public AddUserHandler(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<IdentityResult> Handle(AddUserCommand request, CancellationToken token)
        {
            return await userManager.CreateAsync(request.User, request.Password);
        }
    }
}
