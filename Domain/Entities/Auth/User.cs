using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using Domain.Entities.Surveys;

namespace Domain.Entities.Auth
{
    public class User : IdentityUser<Guid>
    {
        public ICollection<Survey> Surveys { get; set; }
    }
}
