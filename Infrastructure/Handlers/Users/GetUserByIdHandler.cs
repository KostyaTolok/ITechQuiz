using Application.Queries.Users;
using Domain.Entities.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Handlers.Users
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, User>
    {
        private readonly UserManager<User> userManager;

        public GetUserByIdHandler(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<User> Handle(GetUserByIdQuery request, CancellationToken token)
        {
            return await userManager.Users
                                    .Include(user => user.Surveys)
                                    .FirstOrDefaultAsync(user => user.Id == request.Id, token);
        }
    }
}

