using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class OptionDTO
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public bool IsCorrect { get; set; }

        public string Subtitle { get; set; }

        public Guid QuestionId { get; set; }
    }
}
