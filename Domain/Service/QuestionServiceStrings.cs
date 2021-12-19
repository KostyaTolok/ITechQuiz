namespace Domain.Service
{
    public class QuestionServiceStrings
    {
        private const string BaseInternalException = "Internal error occured while ";

        public const string DeleteQuestionException = BaseInternalException + "deleting question";

        public const string AddQuestionException = BaseInternalException + "adding question";
        
        public const string GetQuestionsException = BaseInternalException + "getting questions";
        
        public const string GetQuestionsIdException = "Failed get questions. Wrong Id";
        
        public const string AddQuestionNullException = "Failed to add survey. Survey is null";

        public const string DeleteQuestionIdException = "Failed to delete question. Wrong id";

    }
}