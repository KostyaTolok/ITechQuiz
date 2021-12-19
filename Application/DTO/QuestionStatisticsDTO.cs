using System.Collections;
using System.Collections.Generic;

namespace Application.DTO
{
    public class QuestionStatisticsDTO
    {

        public string QuestionTitle { get; set; }
        
        public bool Required { get; set; }
        
        public IEnumerable<OptionStatisticsDTO> OptionsStatistics { get; set; }
    }
}