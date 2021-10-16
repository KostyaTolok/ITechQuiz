using System.IdentityModel.Tokens.Jwt;
using Domain.Models;
using MediatR;

namespace Application.Commands.Auth
{
    public record CreateTokenCommand(string Email): IRequest<string>;
}