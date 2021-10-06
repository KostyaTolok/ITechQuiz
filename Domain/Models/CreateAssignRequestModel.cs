using Domain.Enums;
using System;

namespace Domain.Models
{
    public class CreateAssignRequestModel
    {
        public Guid UserId { get; set; }

        public string Role { get; set; }
        
    }
}
