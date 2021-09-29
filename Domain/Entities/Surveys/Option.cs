﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Surveys
{
    public class Option
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public bool IsCorrect { get; set; }

        public string Subtitle { get; set; }

        public Question Question { get; set; }

        public Guid QuestionId { get; set; }
    }
}
