using System;
using System.Collections.Generic;
using Domain.Entities.Auth;

namespace Application.DTO
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        
        public string Email { get; set; }
        
        public string UserName { get; set; }
        
        public bool LockoutEnabled { get; set; }
        
        public string LockoutEnd { get; set; }

        public virtual ICollection<AssignRequestDTO> AssignRequests { get; set; }

        public bool IsDisabled { get; set; }

        public string DisabledEnd { get; set; }
        
        public virtual IEnumerable<string> Roles { get; set; }
    }
}