using Domain.Entities.Auth;
using Domain.Enums;
using MediatR;
using System.Collections.Generic;

namespace Application.Queries.Users
{
    public record GetUsersInRoleQuery(Roles Role) : IRequest<IEnumerable<User>>;
}
