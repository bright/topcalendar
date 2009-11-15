namespace TopCalendar.Server.ServiceLibrary.ServiceContract.DataContract.StatusReason
{
    public static class StatusReasonFor
    {
        public static class CheckUser
        {
            public const string USER_NOT_EXIST = "USER_NOT_EXIST";
            public const string PASSWORD_NOT_MATCH = "PASSWORD_NOT_MATCH";
        }

        public static class RegisterUser
        {
            public const string LOGIN_ALREADY_TAKEN = "LOGIN_ALREADY_TAKEN";
        }

        public static class All
        {
            public const string OK = "OK";
            public const string SYSTEM_ERROR = "SYSTEM_ERROR";
        }
    }
}