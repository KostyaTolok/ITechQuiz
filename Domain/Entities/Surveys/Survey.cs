﻿using Domain.Entities.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Surveys
{
    public class Survey
    {
        [Required]
        public Guid Id { get; set; }

        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        [MaxLength(50)]
        [Required]
        public string Title { get; set; }

        [Required]
        public SurveyType Type { get; set; }

        [MaxLength(150)]
        public string Subtitle { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public DateTime CreatedDate { get; set; }

        public ICollection<Question> Questions { get; set; }

        public User User { get; set; }

        public Guid UserId { get; set; }

    }

    public enum SurveyType
    {
        ForStatistics,
        ForQuiz
    }
}