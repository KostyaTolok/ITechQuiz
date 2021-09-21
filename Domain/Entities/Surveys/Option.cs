using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Surveys
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

        public Question Question { get; set; }

        public Guid QuestionId { get; set; }
    }
}
