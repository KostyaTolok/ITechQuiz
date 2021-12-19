using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;

namespace Application.DTO
{
    public class AnswerDTO
    {
        public IEnumerable<OptionDTO> SelectedOptions { get; set; }

        public bool IsAnonymous { get; set; }
        
        public Guid QuestionId { get; set; }
        
        public string QuestionTitle { get; set; }
        
        public bool QuestionRequired { get; set; }
        
        public bool IsAnonymousAllowed { get; set; }
        
        public string CreatedDate { get; set; }
    }
}