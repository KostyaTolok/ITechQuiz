namespace Domain.Service
{
    public static class SurveyServiceStrings
    {
        private const string BaseInternalException = "Internal error occured while ";
        
        private const string BaseUpdateSurveyException = "Failed to update survey. ";

        private const string BaseAddSurveyException = "Failed to add survey. ";

        public const string UpdateSurveyException = BaseInternalException + "updating survey";

        public const string DeleteSurveyException = BaseInternalException + "deleting survey";
        
        public const string AddSurveyException = BaseInternalException + "adding survey";
        
        public const string GetSurveysException = BaseInternalException + "getting surveys";
        
        public const string GetSurveyException = BaseInternalException + "getting survey";

        public const string UpdateSurveyNullException = BaseUpdateSurveyException + "Survey is null";
        
        public const string UpdateSurveyIdException = BaseUpdateSurveyException + "Missing id";
        
        public const string UpdateSurveyQuestionTitleException = BaseUpdateSurveyException + "Missing question title";
        
        public const string UpdateSurveyQuestionIdException = BaseUpdateSurveyException + "Missing question id";

        public const string UpdateSurveyOptionTitleException = BaseUpdateSurveyException + "Missing option title";

        public const string UpdateSurveyOptionIdException = BaseUpdateSurveyException + "Missing option Id";

        public const string UpdateSurveyTitleException = BaseUpdateSurveyException + "Missing title";
        
        public const string UpdateSurveyDateException = BaseUpdateSurveyException + "Missing date of creation";
        
        public const string AddSurveyNullException = BaseAddSurveyException + "Survey is null";
        
        public const string AddSurveyQuestionTitleException = BaseAddSurveyException + "Missing question title";
        
        public const string AddSurveyOptionTitleException = BaseAddSurveyException + "Missing option title";

        public const string AddSurveyTitleException = BaseAddSurveyException + "Missing title";
        
        public const string AddSurveyUserIdException = BaseAddSurveyException + "Missing user id";
        
        public const string AddSurveyDateException = BaseAddSurveyException + "Missing date of creation";

        public const string DeleteSurveyIdException = "Failed to delete survey. Missing id";
        
        public const string GetSurveyIdException = "Failed to get survey. Wrong id";
        
        public const string GetSurveysNullException = "Failed to get surveys";

    }
}