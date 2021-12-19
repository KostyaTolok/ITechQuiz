using System;
using System.Collections;
using System.Collections.Generic;

namespace Domain.Entities.Surveys
{
    public class Category
    {
        public Guid Id { get; set; }
        
        public string Title { get; set; }

        public virtual ICollection<Survey> Surveys { get; set; }
    }
}