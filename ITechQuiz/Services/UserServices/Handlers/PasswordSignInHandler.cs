using ITechQuiz.Auth;
using ITechQuiz.Services.UserServices.Commands;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace ITechQuiz.Services.UserServices.Handlers
{
    public class PasswordSignInUserHandler : IRequestHandler<PasswordSignInUserCommand, SignInResult>
    {
        private readonly UserSignInManager signInManager;

        public PasswordSignInUserHandler(UserSignInManager signInManager)
        {
            this.signInManager = signInManager;
        }

        public async Task<SignInResult> Handle(PasswordSignInUserCommand request, CancellationToken token)
        {
            return await signInManager.PasswordSignInAsync(request.Email, request.Password, request.RememberMe, false);
        }
    }
}
