using Domain.Models;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IAuthService
    {
        public Task Login(LoginModel model);

        public Task Register(RegisterModel model);

        public Task Logout();
    }
}
