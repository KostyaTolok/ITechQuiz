using ITechQuiz.Models;
using MediatR;
using System.Collections.Generic;

namespace ITechQuiz.Services.UserServices.Queries
{
    public record GetUsersQuery() : IRequest<IEnumerable<User>>;
}
