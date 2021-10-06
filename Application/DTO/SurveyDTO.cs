using Domain.Entities.Auth;
using Domain.Enums;
using System;
using System.Collections.Generic;

namespace Application.DTO
{
    public class SurveyDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Title { get; set; }

        public string Type { get; set; }

        public string Subtitle { get; set; }

        public string CreatedDate { get; set; }

        public ICollection<QuestionDTO> Questions { get; set; }

        public User User { get; set; }

        public Guid UserId { get; set; }
    }
}
