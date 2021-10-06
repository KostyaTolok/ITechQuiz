using Domain.Entities.Auth;
using Domain.Enums;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IUsersService
    {
        Task<IEnumerable<User>> GetUsersAsync(string role);

        Task<User> GetUserAsync(Guid id);

        Task<bool> DeleteUserAsync(Guid id);

        Task<bool> DisableUserAsync(DisableUserModel model);

        Task<bool> EnableUserAsync(Guid id);

    }
}
