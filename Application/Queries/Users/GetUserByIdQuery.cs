using Domain.Entities.Auth;
using MediatR;
using System;

namespace Application.Queries.Users
{
    public record GetUserByIdQuery(Guid Id) : IRequest<User>;
}
