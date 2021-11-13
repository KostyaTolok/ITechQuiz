using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Surveys
{
    public class Question
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public bool Multiple { get; set; }

        public int MaxSelected { get; set; }

        public bool Required { get; set; }

        public virtual ICollection<Option> Options { get; set; }

        public Survey Survey { get; set; }

        public Guid SurveyId { get; set; }

    }
}
