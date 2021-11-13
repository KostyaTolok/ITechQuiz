using System.Collections.Generic;
using Domain.Entities.Auth;
using MediatR;

namespace Application.Queries.Users
{
    public record GetRolesByUserQuery(User User) : IRequest<IEnumerable<string>>;
}