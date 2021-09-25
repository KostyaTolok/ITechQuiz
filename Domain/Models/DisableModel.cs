using System;

namespace Domain.Models
{
    public class DisableModel
    {
        public Guid UserId { get; set; }

        public DateTime DisableEnd { get; set; }
    }
}
