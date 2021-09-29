using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using Domain.Entities.Surveys;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Auth
{
    public class User : IdentityUser<Guid>
    {
        public ICollection<Survey> Surveys { get; set; }

        public bool IsDisabled { get; set; }

        public DateTime? DisabledEnd { get; set; }
    }
}
