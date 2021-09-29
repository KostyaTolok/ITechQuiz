using Domain.Entities.Auth;
using Domain.Entities.Surveys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class SurveyDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Title { get; set; }

        public SurveyType Type { get; set; }

        public string Subtitle { get; set; }

        public string CreatedDate { get; set; }

        public ICollection<QuestionDTO> Questions { get; set; }

        public User User { get; set; }

        public Guid UserId { get; set; }
    }
}
