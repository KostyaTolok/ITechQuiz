using Domain.Entities.Auth;
using Domain.Enums;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.DTO;

namespace Application.Interfaces.Services
{
    public interface IUsersService
    {
        Task<IEnumerable<User>> GetUsersAsync(string role);

        Task<UserDTO> GetUserByIdAsync(Guid id);

        Task<UserDTO> GetUserByEmailAsync(string email);

        Task<bool> DeleteUserAsync(Guid id);

        Task<bool> DisableUserAsync(DisableUserModel model);

        Task<bool> EnableUserAsync(Guid id);

        Task<bool> RemoveUserFromRoleAsync(RemoveUserFromRoleModel model,string currentEmail,
            CancellationToken token);

        Task<Guid?> GetUserIdByEmail(string userEmail, CancellationToken token);

        Task<string> GetUserEmailBySurveyId(Guid surveyId, CancellationToken token);
    }
}