using ITechQuiz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITechQuiz.Services.AuthServices
{
    public interface IAuthService
    {
        public Task Login(LoginModel model);

        public Task Register(RegisterModel model);

        public Task Logout();
    }
}
