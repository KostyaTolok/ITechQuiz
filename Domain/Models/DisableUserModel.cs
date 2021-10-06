using System;

namespace Domain.Models
{
    public class DisableUserModel
    {
        public Guid UserId { get; set; }

        public DateTime? DisableEnd { get; set; }
    }
}
