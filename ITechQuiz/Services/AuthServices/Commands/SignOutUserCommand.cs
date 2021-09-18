using MediatR;

namespace ITechQuiz.Services.AuthServices.Commands
{
    public record SignOutUserCommand() : IRequest<Unit>;
}
