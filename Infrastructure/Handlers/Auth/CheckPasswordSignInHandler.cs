using Application.Commands.Auth;
using Domain.Entities.Auth;
using Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Handlers.Auth
{
    public class CheckPasswordSignInHandler : IRequestHandler<CheckPasswordSignInCommand, SignInResult>
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;

        public CheckPasswordSignInHandler(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        public async Task<SignInResult> Handle(CheckPasswordSignInCommand request, CancellationToken token)
        {
            var user = await userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                return SignInResult.Failed;
            }

            if (await userManager.IsLockedOutAsync(user))
            {
                return SignInResult.LockedOut;
            }

            if (user.IsDisabled)
            {
                if (!(user.DisabledEnd.HasValue && user.DisabledEnd < System.DateTime.Now))
                {
                    return SignInResult.NotAllowed;
                }
                else
                {
                    user.IsDisabled = false;
                    await userManager.UpdateAsync(user);
                }
            }

            return await signInManager.CheckPasswordSignInAsync(user, request.Password, true);
        }
    }
}
