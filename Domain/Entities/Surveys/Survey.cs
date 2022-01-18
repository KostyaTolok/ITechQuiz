using Domain.Entities.Auth;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Entities.Surveys
{
    public class Survey : IEquatable<Survey>
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

        public override bool Equals(object obj)
        {
            return Equals(obj as Survey);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public bool Equals(Survey other)
        {
            if (other == null)
                return false;

            return Id == other.Id && Title == other.Title &&
                   Type == other.Type && Subtitle == other.Subtitle &&
                   UpdatedDate == other.UpdatedDate &&
                   IsAnonymousAllowed == other.IsAnonymousAllowed &&
                   IsMultipleAnswersAllowed == other.IsMultipleAnswersAllowed &&
                   Questions.SequenceEqual(other.Questions);
        }
    }
}