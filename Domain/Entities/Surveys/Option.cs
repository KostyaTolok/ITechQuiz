using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Surveys
{
    public class Option : IEquatable<Option>
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public bool IsCorrect { get; set; }

        public string Subtitle { get; set; }

        public Question Question { get; set; }

        public Guid QuestionId { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Option);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public bool Equals(Option other)
        {
            if (other == null)
                return false;
            
            return Id == other.Id && Title == other.Title &&
                   IsCorrect == other.IsCorrect && Subtitle == other.Subtitle &&
                   QuestionId == other.QuestionId;
        }
    }
}
