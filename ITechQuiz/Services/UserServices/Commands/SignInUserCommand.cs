using ITechQuiz.Models;
using MediatR;

namespace ITechQuiz.Services.UserServices.Commands
{
    public record SignInUserCommand(User User) : IRequest<Unit>;
}
