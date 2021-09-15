using ITechQuiz.Models;
using ITechQuiz.Services.UserServices.Queries;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ITechQuiz.Services.UserServices.Handlers
{
    public class GetUsersHandler : IRequestHandler<GetUsersQuery, IEnumerable<User>>
    {
        private readonly UserManager<User> userManager;

        public GetUsersHandler(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<IEnumerable<User>> Handle(GetUsersQuery request, CancellationToken token)
        {
            return await userManager.Users.ToListAsync(token);
        }
    }
}
