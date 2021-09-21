using Domain.Entities.Auth;
using MediatR;
using System.Collections.Generic;

namespace Application.Queries.Users
{
    public record GetUsersQuery() : IRequest<IEnumerable<User>>;
}
