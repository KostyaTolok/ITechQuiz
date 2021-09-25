using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Application.Commands.Auth;
using Infrastructure.Services;
using Domain.Entities.Auth;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Handlers.Auth
{
    public class SignInUserHandler : IRequestHandler<SignInUserCommand, Unit>
    {
        private readonly SignInManager<User> signInManager;

        public SignInUserHandler(SignInManager<User> signInManager)
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
