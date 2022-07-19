using MPR.Shared.Logic.Errors;
using MPR.Shared.Logic.Responses;

namespace MPR.Shared.Logic.Extensions
{
    public static class ExceptionExtensions
    {
        public static Response<T> ToResponse<T>(this Exception exception)
        {
            return Response.GetResponseFromError<T>(errorCode: SharedErrorCodes.UNEXPECTED_ERROR, errorDescription: exception.ToString());
        }

        public static Response ToResponse(this Exception exception)
        {
            return Response.GetResponseFromError(errorCode: SharedErrorCodes.UNEXPECTED_ERROR, errorDescription: exception.ToString());
        }
    }
}
