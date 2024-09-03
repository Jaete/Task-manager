using System.Net;

namespace BE_TaskManager.Models.Response{
    
    public class Response {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;

        public Response(HttpStatusCode statusCode, string message) {
            StatusCode = statusCode;
            Message = message;
        }
    }

    public class FetchResponse<T> : Response
    {
        public T? Data { get; set; }
        public FetchResponse(T data, HttpStatusCode statusCode, string message) : base(statusCode, message)
        {
            Data = data;
            StatusCode = statusCode;
            Message = message;
        }
    }
}