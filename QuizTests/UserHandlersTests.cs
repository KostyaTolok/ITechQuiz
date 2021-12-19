using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.Users;
using Domain.Entities.Auth;
using Infrastructure.Handlers.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Application.UnitTests
{
    public class UserHandlersTests
    {
        private readonly Mock<UserManager<User>> userManager = new();
        private readonly IEnumerable<User> users = TestData.GetTestUsers();
        private readonly User user = TestData.GetTestUsers()[0];
        
        public async Task DisableUserHandlerTest()
        {
            userManager.Setup(m=>m.Users.FirstOrDefaultAsync(default)).ReturnsAsync(user).Verifiable();
            
            var handler = new DisableUserHandler(userManager.Object);
            var actual = await handler.Handle(new DisableUserCommand(user.Id, null), CancellationToken.None);

            userManager.Verify();

            Assert.True(actual);
        }
    }
}