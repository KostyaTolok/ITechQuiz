using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Queries.Users;
using Domain.Entities.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Handlers.Users
{
    public class GetRolesByUserHandler: IRequestHandler<GetRolesByUserQuery, IEnumerable<string>>
    {
        private readonly UserManager<User> userManager;

        public GetRolesByUserHandler(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<IEnumerable<string>> Handle(GetRolesByUserQuery request, CancellationToken token)
        {
            return await userManager.GetRolesAsync(request.User);
        }
    }
}