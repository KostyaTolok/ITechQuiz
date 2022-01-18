using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.Auth;
using Domain.Entities.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Handlers.Auth
{
    public class CreateTokenHandler : IRequestHandler<CreateTokenCommand, string>
    {
        private readonly IConfiguration configuration;
        private readonly UserManager<User> userManager;

        public CreateTokenHandler(IConfiguration configuration,
            UserManager<User> userManager)
        {
            this.configuration = configuration;
            this.userManager = userManager;
        }

        public async Task<string> Handle(CreateTokenCommand request,
            CancellationToken token)
        {
            var user = await userManager.FindByEmailAsync(request.Email);

            var roles = await userManager.GetRolesAsync(user);
            
            var claims = new List<Claim>
            {
                new (JwtRegisteredClaimNames.Sub, request.Email),
                new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim("roles", role));
            }

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration["Token:Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken(
                configuration["Token:Issuer"],
                configuration["Token:Audience"],
                claims, signingCredentials: creds,
                expires: DateTime.UtcNow.AddYears
                    (Convert.ToInt32(configuration["Token:Lifetime"]))
            );
            
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}