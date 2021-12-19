namespace Domain.Service
{
    public static class AnswerServiceStrings
    {
        private const string BaseInternalException = "Internal error occured while ";

        public const string AddAnswerException = BaseInternalException + "adding answer";
        
        public const string GetAnswersException = BaseInternalException + "getting answers";
        
        public const string AddAnswerQuestionIdException = "Failed to add answer. Wrong question id";
        
        public const string AddAnswerOptionIdException = "Failed to add answer. Wrong option id";
        
        public const string AddAnswerAnonymousException = "Failed to add anonymous answer. Anonymous is not allowed";
        
        public const string AddAnswerMultipleException = "Failed to add answer. Multiple answers are not allowed";
        
        public const string GetAnswersNullException = "Failed to get answers";
    }
}