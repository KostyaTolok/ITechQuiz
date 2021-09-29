using Domain.Entities.Auth;
using Domain.Enums;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IUserService
    {

        Task<IEnumerable<User>> GetUsersAsync(Roles? role);

        Task<User> GetUserAsync(Guid id);

        Task<bool> DeleteUserAsync(Guid id);

        Task<bool> AddToRoleAsync(AddToRoleModel model);

        Task<bool> DisableUserAsync(DisableModel model);

        Task<bool> EnableUserAsync(Guid id);

    }
}
