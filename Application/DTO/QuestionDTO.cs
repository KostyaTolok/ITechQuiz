using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class QuestionDTO
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public bool Multiple { get; set; }

        public int MaxSelected { get; set; }

        public bool Required { get; set; }

        public ICollection<OptionDTO> Options { get; set; }

        public Guid SurveyId { get; set; }
    }
}
