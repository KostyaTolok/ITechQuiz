using Domain.Entities.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    [ExcludeFromCodeCoverage]
    public class UserSignInManager : SignInManager<User>
    {
        public UserSignInManager(UserManager<User> userManager, IHttpContextAccessor contextAccessor,
            IUserClaimsPrincipalFactory<User> claimsFactory, IOptions<IdentityOptions> optionsAccessor,
            ILogger<SignInManager<User>> logger, IAuthenticationSchemeProvider schemes)
        : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes)
        {

        }

        public override async Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure)
        {
            var user = await UserManager.FindByEmailAsync(userName);
            if (user == null)
            {
                return SignInResult.Failed;
            }

            return await PasswordSignInAsync(user, password, isPersistent, lockoutOnFailure);
        }
    }
}
