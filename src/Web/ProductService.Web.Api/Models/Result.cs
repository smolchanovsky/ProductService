using System.Net;

namespace ProductService.Web.Api.Models
{
    public class Result<T> : Result where T : class
    {
        public T? Data { get; protected set; }

        protected Result(HttpStatusCode statusCode, string? message = null, T? data = null) : base(statusCode)
        {
            StatusCode = statusCode;
            Message = message;
            Data = data;
        }

        public static Result<T> Success(HttpStatusCode statusCode, T data)
        {
            return new Result<T>(statusCode, data: data);
        }

        public new static Result<T> Failure(HttpStatusCode statusCode, string message)
        {
            return new Result<T>(statusCode, message);
        }
    }

    public class Result
    {
        public HttpStatusCode StatusCode { get; protected set; }
        public string? Message { get; protected set; }

        protected Result(HttpStatusCode statusCode, string? message = null)
        {
            StatusCode = statusCode;
            Message = message;
        }

        public static Result Success(HttpStatusCode statusCode)
        {
            return new Result(statusCode);
        }

        public static Result Failure(HttpStatusCode statusCode, string message)
        {
            return new Result(statusCode, message);
        }
    }
}
