using Microsoft.AspNetCore.Mvc;

namespace MPR.Shared.Logic.Responses
{
    public class MprProblemDetails : ValidationProblemDetails
    {
        public string ErrorCode { get; set; }
    }
}
