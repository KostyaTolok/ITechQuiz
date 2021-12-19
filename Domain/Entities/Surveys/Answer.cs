using System;
using System.Collections;
using System.Collections.Generic;
using Domain.Entities.Auth;

namespace Domain.Entities.Surveys
{
    public class Answer
    {
        public Guid Id { get; set; }
        
        public Guid QuestionId { get; set; }
        
        public Question Question { get; set; }

        public virtual ICollection<Option> SelectedOptions { get; set; }
        
        public Guid? UserId { get; set; }
        
        public User User { get; set; }
        
        public bool IsAnonymous { get; set; }
        
        public DateTime CreatedDate { get; set; }
    }
}