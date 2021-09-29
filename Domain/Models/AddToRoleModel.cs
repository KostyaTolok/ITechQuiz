using Domain.Enums;
using System;

namespace Domain.Models
{
    public class AddToRoleModel
    {
        public Guid UserId { get; set; }

        public Roles Role { get; set; }
    }

}
