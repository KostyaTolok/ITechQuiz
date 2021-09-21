using MediatR;

namespace Application.Commands.Auth
{
    public record SignOutUserCommand() : IRequest<Unit>;
}
