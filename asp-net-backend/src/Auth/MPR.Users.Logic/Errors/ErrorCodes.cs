using MPR.Shared.Logic.Errors;

namespace MPR.Users.Logic.Errors
{
    public class ErrorCodes : SharedErrorCodes
    {
        public const string EMAIL_ALREADYEXISTS = "EMAIL_ALREADYEXISTS";
        public const string INVALID_USER = "INVALID_USER";
        public const string USER_NOTACTIVE = "USER_NOTACTIVE";
        public const string USER_ALREADYREGISTER = "USER_ALREADYREGISTER";
        public const string INVALID_TOKEN = "INVALID_TOKEN";
    }
}
