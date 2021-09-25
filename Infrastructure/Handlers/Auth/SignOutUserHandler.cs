using Application.Commands.Auth;
using Domain.Entities.Auth;
using Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Handlers.Auth
{
    public class SignOutUserHandler : IRequestHandler<SignOutUserCommand, Unit>
    {
        private readonly SignInManager<User> signInManager;

        public SignOutUserHandler(SignInManager<User> signInManager)
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
