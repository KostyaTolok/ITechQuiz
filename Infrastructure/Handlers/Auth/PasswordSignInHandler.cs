using Application.Commands.Auth;
using Domain.Entities.Auth;
using Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Handlers.Auth
{
    public class PasswordSignInUserHandler : IRequestHandler<PasswordSignInUserCommand, SignInResult>
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;

        public PasswordSignInUserHandler(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        public async Task<SignInResult> Handle(PasswordSignInUserCommand request, CancellationToken token)
        {
            var user = await userManager.FindByEmailAsync(request.Email);
            if (await userManager.IsLockedOutAsync(user))
            {
                return SignInResult.LockedOut;
            }
            if (user == null)
            {
                return SignInResult.Failed;
            }
            return await signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, false);
        }
    }
}
