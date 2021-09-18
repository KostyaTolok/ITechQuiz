using ITechQuiz.Models;
using ITechQuiz.Services.AuthServices.Queries;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace ITechQuiz.Services.AuthServices.Handlers
{
    public class GetUserByEmailHandler : IRequestHandler<GetUserByEmailQuery, User>
    {
        private readonly UserManager<User> userManager;

        public GetUserByEmailHandler(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<User> Handle(GetUserByEmailQuery request, CancellationToken token)
        {
            return await userManager.Users
                                    .Include(user => user.Surveys)
                                    .FirstOrDefaultAsync(user => user.Email == request.Email, token);
        }
    }
}
