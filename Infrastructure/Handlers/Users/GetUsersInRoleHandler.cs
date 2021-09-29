using Application.Queries.Users;
using Domain.Entities.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Handlers.Users
{
    public class GetUsersInRoleHandler : IRequestHandler<GetUsersInRoleQuery, IEnumerable<User>>
    {
        private readonly UserManager<User> userManager;

        public GetUsersInRoleHandler(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<IEnumerable<User>> Handle(GetUsersInRoleQuery request, CancellationToken token)
        {
            return await userManager.GetUsersInRoleAsync(request.Role.ToString());
        }
    }
}
