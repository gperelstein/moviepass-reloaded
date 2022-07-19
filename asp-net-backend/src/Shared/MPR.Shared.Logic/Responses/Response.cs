using System.Net;

namespace MPR.Shared.Logic.Responses
{
    public class Response<T> : Response
    {
        public T Payload { get; set; }

        public Response() { }

        public Response(HttpStatusCode statusCode) : base(statusCode) { }
    }
}
