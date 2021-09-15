using MediatR;
using Microsoft.AspNetCore.Identity;
using System;

namespace ITechQuiz.Services.UserServices.Commands
{
    public record DeleteUserCommand(Guid Id) : IRequest<IdentityResult>;

}
