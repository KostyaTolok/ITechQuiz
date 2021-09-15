using ITechQuiz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITechQuiz.Services.UserServices
{
    public interface IUserService
    {
        public Task<IEnumerable<User>> GetUsersAsync();
        public Task<User> GetUserAsync(Guid id);

        public Task Login(LoginModel model);

        public Task Register(RegisterModel model);

        public Task LogOut();

        public Task DeleteUserAsync(Guid id);

    }
}
