using Domain.Enums;
using System;

namespace Domain.Entities.Auth
{
    public class AssignRequest
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }

        public Roles UserRole { get; set; }

        public DateTime CreatedDate { get; set; }
        
        public bool IsRejected { get; set; }
    }
}
