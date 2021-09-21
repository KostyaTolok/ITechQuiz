using Domain.Entities.Auth;
using MediatR;

namespace Application.Queries.Auth
{
    public record GetUserByEmailQuery(string Email) : IRequest<User>;
}
