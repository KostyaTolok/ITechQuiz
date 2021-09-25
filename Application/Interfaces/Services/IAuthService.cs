using Domain.Models;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task Login(LoginModel model);

        Task Register(RegisterModel model);

        Task Logout();
    }
}
