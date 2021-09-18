using ITechQuiz.Models;
using MediatR;

namespace ITechQuiz.Services.AuthServices.Commands
{
    public record SignInUserCommand(User User) : IRequest<Unit>;
}
