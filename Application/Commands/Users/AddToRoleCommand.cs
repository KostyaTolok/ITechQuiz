using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Users
{
    public record AddToRoleCommand(Guid Id, string Role) : IRequest<IdentityResult>;
}
