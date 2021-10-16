using Application.Commands.Auth;
using Domain.Entities.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Handlers.Users
{
    public class AddUserHandler : IRequestHandler<CreateUserCommand, IdentityResult>
    {
        private readonly UserManager<User> userManager;

        public AddUserHandler(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<IdentityResult> Handle(CreateUserCommand request, CancellationToken token)
        {
            return await userManager.CreateAsync(request.User, request.Password);
        }
    }
}
