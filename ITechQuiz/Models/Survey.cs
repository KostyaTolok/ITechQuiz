using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ITechQuiz.Models
{
    public class Survey
    {
        [Key]
        [Required]
        public string Name { get; set; }

        [MaxLength(50)]
        [Required]
        public string Title { get; set; }

        [Required]
        public Type Type { get; set; }

        [MaxLength(150)]
        public string Subtitle { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public DateTime CreatedDate { get; set; }

        public List<Question> Questions { get; set; }

    }

    public enum Type
    {
        ForStatistics,
        ForQuiz
    }
}
