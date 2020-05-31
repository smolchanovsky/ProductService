using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ProductService.Infrastructure.Serialization.Json;
using ProductService.Web.Api.Models;

namespace ProductService.Web.Api.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context, IJsonSerializer jsonSerializer, ILogger<ErrorHandlingMiddleware> logger)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An unexpected error occurred");

                var result = Result.Failure(HttpStatusCode.InternalServerError, message: "Internal server error.");
                var json = jsonSerializer.Serialize(result);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.OK;
                await context.Response.WriteAsync(json).ConfigureAwait(false);
            }
        }
    }
}
