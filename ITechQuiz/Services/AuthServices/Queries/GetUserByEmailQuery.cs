using ITechQuiz.Models;
using MediatR;

namespace ITechQuiz.Services.AuthServices.Queries
{
    public record GetUserByEmailQuery(string Email) : IRequest<User>;
}
