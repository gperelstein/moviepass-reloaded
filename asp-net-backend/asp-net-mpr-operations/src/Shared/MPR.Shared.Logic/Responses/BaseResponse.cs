using Microsoft.AspNetCore.Mvc;
using MPR.Shared.Logic.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MPR.Shared.Logic.Responses
{
    public class Response
    {
        public const string ValidationErrorTitle = "One or more validation errors occurred.";
        public const string DefaultErrorTitle = "Something went wrong.";

        public Response()
        {
            StatusCode = HttpStatusCode.OK;
            ValidationIssue = new MprProblemDetails
            {
                Title = DefaultErrorTitle
            };
        }

        public Response(HttpStatusCode statusCode)
            : this()
        {
            StatusCode = statusCode;
        }

        public ValidationProblemDetails ValidationIssue { get; set; }

        public IDictionary<string, string[]> Errors
        {
            get { return ValidationIssue.Errors; }
        }

        public HttpStatusCode StatusCode { get; set; }

        public bool IsSuccessStatusCode
            => (int)StatusCode >= 200 && (int)StatusCode <= 299;

        public static Response CreateBadRequestResponse(string code, string description)
        {
            return new Response
            {
                ValidationIssue = GetValidationIssue(code, description, HttpStatusCode.BadRequest, ValidationErrorTitle),
                StatusCode = HttpStatusCode.BadRequest
            };
        }

        public static Response<T> CreateBadRequestResponse<T>(string code, string description)
        {
            return new Response<T>
            {
                ValidationIssue = GetValidationIssue(code, description, HttpStatusCode.BadRequest, ValidationErrorTitle),
                StatusCode = HttpStatusCode.BadRequest
            };
        }

        public static Response CreateBadRequestResponse(IDictionary<string, string[]> errorList)
        {
            return new Response
            {
                ValidationIssue = new ValidationProblemDetails(errorList)
                {
                    Title = ValidationErrorTitle,
                    Status = (int)HttpStatusCode.BadRequest
                },
                StatusCode = HttpStatusCode.BadRequest
            };
        }

        public static Response<T> CreateBadRequestResponse<T>(IDictionary<string, string[]> errorList)
        {
            return new Response<T>
            {
                ValidationIssue = new ValidationProblemDetails(errorList)
                {
                    Title = ValidationErrorTitle,
                    Status = (int)HttpStatusCode.BadRequest
                },
                StatusCode = HttpStatusCode.BadRequest
            };
        }

        public static Response CreateUnauthorizedResponse(string code, string description)
        {
            return new Response
            {
                ValidationIssue = GetValidationIssue(code, description, HttpStatusCode.Unauthorized, ValidationErrorTitle),
                StatusCode = HttpStatusCode.Unauthorized
            };
        }

        public static Response<T> CreateUnauthorizedResponse<T>(string code, string description)
        {
            return new Response<T>
            {
                ValidationIssue = GetValidationIssue(code, description, HttpStatusCode.Unauthorized, ValidationErrorTitle),
                StatusCode = HttpStatusCode.Unauthorized
            };
        }

        public static Response GetResponseFromError(
                HttpStatusCode statusCode = HttpStatusCode.InternalServerError,
                string errorCode = SharedErrorCodes.UNEXPECTED_ERROR,
                string errorDescription = null)
        {
            return new Response(statusCode)
            {
                ValidationIssue = GetValidationIssue(errorCode, errorDescription, statusCode, DefaultErrorTitle),
                StatusCode = statusCode
            };
        }

        public static Response<T> GetResponseFromError<T>(
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError,
            string errorCode = SharedErrorCodes.UNEXPECTED_ERROR,
            string errorDescription = null)
        {
            return new Response<T>(statusCode)
            {
                ValidationIssue = GetValidationIssue(errorCode, errorDescription, statusCode, DefaultErrorTitle),
                StatusCode = statusCode
            };
        }

        private static ValidationProblemDetails GetValidationIssue(string code, string description, HttpStatusCode statusCode, string title)
        {
            return new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                { code, new string[] { description } }
            })
            {
                Title = title,
                Status = (int)statusCode
            };
        }
    }
}
