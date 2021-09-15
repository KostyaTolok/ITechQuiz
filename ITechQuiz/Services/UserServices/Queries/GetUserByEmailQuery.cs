using ITechQuiz.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITechQuiz.Services.UserServices.Queries
{
    public record GetUserByEmailQuery(string Email) : IRequest<User>;
}
