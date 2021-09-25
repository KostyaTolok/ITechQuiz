using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class AddToRoleModel
    {
        public Guid UserId { get; set; }

        public Roles Role { get; set; }
    }

    public enum Roles
    {
        Admin,
        Client
    }
}
