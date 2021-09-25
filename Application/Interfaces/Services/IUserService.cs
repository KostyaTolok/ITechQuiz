using Domain.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IUserService
    {

        Task<IEnumerable<User>> GetUsersAsync();

        Task<User> GetUserAsync(Guid id);

        Task<bool> DeleteUserAsync(Guid id);

        Task<bool> AddToClientRoleAsync(Guid id);

        Task<bool> LockoutUserAsync(Guid id);

    }
}
