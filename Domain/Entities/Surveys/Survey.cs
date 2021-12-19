using Domain.Entities.Auth;
using Domain.Enums;
using System;
using System.Collections.Generic;

namespace Domain.Entities.Surveys
{
    public class Survey
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public SurveyTypes Type { get; set; }

        public string Subtitle { get; set; }

        public DateTime CreatedDate { get; set; }
        
        public DateTime UpdatedDate { get; set; }

        public virtual ICollection<Question> Questions { get; set; }

        public User User { get; set; }

        public Guid UserId { get; set; }
        
        public bool IsAnonymousAllowed { get; set; }
        
        public bool IsMultipleAnswersAllowed { get; set; }

        public virtual ICollection<Category> Categories { get; set; }

    }

}
