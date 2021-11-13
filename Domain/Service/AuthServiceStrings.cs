namespace Domain.Service
{
    public static class AuthServiceStrings
    {
        private const string BaseInternalException = "Internal error occured while";

        public const string LoginException = BaseInternalException + "logging";

        public const string RegisterException = BaseInternalException + "registration";

        public const string LogoutException = BaseInternalException + "logging out";
        
        public const string ChangePasswordException = BaseInternalException + "changing password";

        public const string UserDisabledException = "User is disabled";
        
        public const string UserLockedOutException = "User is locked out";
        
        public const string UserSignInException = "User failed to sign in";
        
        public const string UserLoginOrPasswordException = "Wrong login or password";
        
        public const string UserSignInInformation = "User signed in";
        
        public const string RegisterFailedException = "Failed to register";
        
        public const string UserExistsException = "User with this email or password already exists";

        public const string ChangePasswordExceptionUserNotFound = "Failed to change password. User not found";
        
        public const string ChangePasswordExceptionMismatch = "Failed to change password. Passwords don't match";
        
        public const string PasswordChangeInformation = "Password successfully changed";
    }
}