using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Domain.Entities.Surveys
{
    public class Question : IEquatable<Question>
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public bool Multiple { get; set; }

        public int MaxSelected { get; set; }

        public bool Required { get; set; }

        public virtual ICollection<Option> Options { get; set; }

        public Survey Survey { get; set; }

        public Guid SurveyId { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Question);
        }
        
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public bool Equals(Question other)
        {
            if (other == null)
                return false;
            
            return Id == other.Id && Title == other.Title &&
                   Multiple == other.Multiple && MaxSelected == other.MaxSelected &&
                   Required == other.Required && SurveyId == other.SurveyId &&
                   Options.SequenceEqual(other.Options);
        }
    }
}
