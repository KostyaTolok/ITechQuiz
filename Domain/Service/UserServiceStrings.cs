namespace Domain.Service
{
    public static class UserServiceStrings
    {
        private const string BaseInternalException = "Internal error occured while ";
        
        public const string GetUsersException = BaseInternalException + "getting users";
        
        public const string GetRolesException = BaseInternalException + "getting roles";
        
        public const string GetUserException = BaseInternalException + "getting user";
        
        public const string DeleteUserException = BaseInternalException + "deleting user";
        
        public const string DisableUserException = BaseInternalException + "disabling user";
        
        public const string EnableUserException = BaseInternalException + "enabling user";
        
        public const string RemoveUserFromRoleException = BaseInternalException + "removing user from role";

        public const string GetUsersRoleException = "Failed to get users. Role is incorrect";
        
        public const string GetUsersNullException = "Failed to get users.";
        
        public const string GetUserIdException = "Failed to get user. Wrong id";
        
        public const string GetUserEmailException = "Failed to get user. Wrong email";

        public const string DeleteUserIdException = "Failed to get user. Wrong id";

        public const string UserDeletedInformation = "User successfully deleted";
        
        public const string DisableUserTimeException = "Failed to disable user. Disable end time is incorrect";
        
        public const string DisableUserIdException = "Failed to disable user. Wrong id";
        
        public const string UserDisabledInformation = "User successfully disabled";
        
        public const string EnableUserIdException = "Failed to enable user. Wrong id";

        public const string UserEnabledInformation = "User successfully enabled";
        
        public const string RemoveUserFromRoleExceptionRole = "Failed to remove user from role. Wrong role";
        
        public const string RemoveUserFromRoleIdException = "Failed to remove user from role. Wrong id";
        
        public const string UserRemovedFromRoleInformation = "User successfully removed from role";
        
        public const string RemoveFromRoleCurrentRoleException = "Failed to remove user from his current role.";
    }
}