using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Application.Commands.Auth;
using Infrastructure.Services;

namespace Infrastructure.Handlers.Auth
{
    public class SignInUserHandler : IRequestHandler<SignInUserCommand, Unit>
    {
        private readonly UserSignInManager signInManager;

        public SignInUserHandler(UserSignInManager signInManager)
        {
            this.signInManager = signInManager;
        }

        public async Task<Unit> Handle(SignInUserCommand request, CancellationToken token)
        {
            await signInManager.SignInAsync(request.User, false);

            return Unit.Value;
        }
    }
}
