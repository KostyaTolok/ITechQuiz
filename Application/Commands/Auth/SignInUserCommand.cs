using Domain.Entities.Auth;
using MediatR;

namespace Application.Commands.Auth
{
    public record SignInUserCommand(User User) : IRequest<Unit>;
}
