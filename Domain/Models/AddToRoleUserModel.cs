using Domain.Enums;
using System;

namespace Domain.Models
{
    public class AddToRoleUserModel
    {
        public Guid UserId { get; set; }

        public Roles Role { get; set; }
    }

}
