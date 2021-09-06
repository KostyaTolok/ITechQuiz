using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace ITechQuiz.Models
{
    public class Option
    {
        [Required]
        public Guid Id { get; set; }

        [MaxLength(100)]
        [Required]
        public string Title { get; set; }

        [Required]
        public bool IsCorrect { get; set; }

        [MaxLength(150)]
        public string Subtitle { get; set; }
    }
}
