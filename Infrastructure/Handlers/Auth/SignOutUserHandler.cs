using Application.Commands.Auth;
using Infrastructure.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Handlers.Auth
{
    public class SignOutUserHandler : IRequestHandler<SignOutUserCommand, Unit>
    {
        private readonly UserSignInManager signInManager;

        public SignOutUserHandler(UserSignInManager signInManager)
        {
            this.signInManager = signInManager;
        }

        public async Task<Unit> Handle(SignOutUserCommand request, CancellationToken token)
        {
            await signInManager.SignOutAsync();

            return Unit.Value;
        }
    }
}
