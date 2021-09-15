using ITechQuiz.Models;
using MediatR;
using System;

namespace ITechQuiz.Services.UserServices.Queries
{
    public record GetUserByIdQuery(Guid Id) : IRequest<User>;
}
