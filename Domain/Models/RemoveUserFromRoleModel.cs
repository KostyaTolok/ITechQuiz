using System;

namespace Domain.Models
{
    public class RemoveUserFromRoleModel
    {
        public Guid UserId { get; set; }

        public string Role { get; set; }
    }
}