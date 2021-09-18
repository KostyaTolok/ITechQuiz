using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ITechQuiz.Models
{
    public class Question
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public bool Multiple { get; set; }

        [Required]
        public int MaxSelected { get; set; }

        [Required]
        public bool Required { get; set; }

        public ICollection<Option> Options { get; set; }

        public Survey Survey { get; set; }

        public Guid SurveyId { get; set; }

    }
}
