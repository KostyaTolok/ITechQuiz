using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class AssignRequestDTO
    {
        public string UserName { get; set; }

        public string RoleName { get; set; }

        public Guid Id { get; set; }
        
        public bool IsRejected { get; set; }
        
        public string CreatedDate { get; set; }
    }
}
