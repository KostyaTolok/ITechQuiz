using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Domain.Entities.Auth
{
    public class Role : IdentityRole<Guid>
    {
        public string Description { get; set; }
        
    }
}
