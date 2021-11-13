namespace Domain.Service
{
    public class AssignRequestsServiceStrings
    {
        private const string BaseInternalException = "Internal error occured while";

        public const string AddAssignRequestException = BaseInternalException + "adding assign request";
        
        public const string DeleteAssignRequestException = BaseInternalException + "deleting assign request";

        public const string GetAssignRequestException = BaseInternalException + "getting assign request";

        public const string GetAssignRequestsException = BaseInternalException + "getting assign requests";
        
        public const string UpdateAssignRequestException = BaseInternalException + "updating assign request";

        public const string AddAssignRequestNullException = "Failed to add assign request. Assign request is null";

        public const string AddAssignRequestUserIdException = "Failed to add assign request. Missing user";

        public const string AddAssignRequestRoleException = "Failed to add assign request. Incorrect role";

        public const string DeleteAssignRequestIdException = "Failed to delete assign request. Wrong id";

        public const string GetAssignRequestsNullException = "Failed to get assign request";

        public const string AddToRoleException = BaseInternalException + "adding user to role";
        
        public const string AddToRoleIdException = "Failed to add user to role. Wrong id";

        public const string AddToRoleIdInformation = "User added to role";

    }
}