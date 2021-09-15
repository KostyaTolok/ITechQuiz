using MediatR;

namespace ITechQuiz.Services.UserServices.Commands
{
    public record SignOutUserCommand() : IRequest<Unit>;
}
