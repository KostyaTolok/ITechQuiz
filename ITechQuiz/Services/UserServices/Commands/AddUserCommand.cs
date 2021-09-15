﻿using ITechQuiz.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ITechQuiz.Services.UserServices.Commands
{
    public record AddUserCommand(User User, string Password) : IRequest<IdentityResult>;
}
