namespace Diary.Api.Consts
{
    public sealed  class ResponseMessages
    {
        public const string Success = "Successfully";
        public const string Failed = "Failled";
        public const string Wrong = "Something Went Wrong";
        public const string Waiting = "The Process Waiting";
        public const string InvalidContentType = "Invalid Content Type";
        public const string InvalidRole = "Invalid Role";
        public const string UnauthorizedAccess = "Unauthorized Access";
        public const string AuthenticateFailed = "Authenticate Failled";
        public const string AuthenticateSuccess = "Authenticate Successfully";
        public const string NotFound = "Not Found Object";
        public const string NotFoundApiKey = "Api Key Not Found";
        public const string LessClaim = "Api Key Have Less Claim";
    }
}