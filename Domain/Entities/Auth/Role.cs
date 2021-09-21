using Microsoft.AspNetCore.Identity;
using System;


namespace Domain.Entities.Auth
{
    public class Role : IdentityRole<Guid>
    {
        public string Description { get; set; }
    }
}
