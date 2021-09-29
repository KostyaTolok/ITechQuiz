﻿using Domain.Entities.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Surveys
{
    public class Survey
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Title { get; set; }

        public SurveyType Type { get; set; }

        public string Subtitle { get; set; }

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
