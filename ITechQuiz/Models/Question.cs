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

        public List<Option> Options { get; set; }

    }
}
