using ITechQuiz.Services.UserServices.Commands;
using MediatR;
using ITechQuiz.Auth;
using System.Threading.Tasks;
using System.Threading;

namespace ITechQuiz.Services.UserServices.Handlers
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
