using System.Threading;
using Domain.Models;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<string> Login(LoginModel model);

        Task<string> Register(RegisterModel model);

        Task Logout();
        
        Task ChangePasswordAsync(ChangePasswordModel model, CancellationToken token);
    }
}
