using Domain.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IUserService
    {

        public Task<IEnumerable<User>> GetUsersAsync();

        public Task<User> GetUserAsync(Guid id);

        public Task DeleteUserAsync(Guid id);

    }
}
