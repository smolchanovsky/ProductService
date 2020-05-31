using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProductService.Web.Api.Auth;
using ProductService.Web.Api.Models;

namespace ProductService.Web.Api.Filters
{
    public class AuthorizationFilter : IAuthorizationFilter
    {
        private readonly IAuthorizationService authorizationService;

        public AuthorizationFilter(IAuthorizationService authorizationService)
        {
            this.authorizationService = authorizationService;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var authHeader = context.HttpContext.Request.Headers["Authorization"];
            if (authorizationService.TryAuthorize(authHeader))
                return;

            var result = Result.Failure(HttpStatusCode.Unauthorized, message: "Access is denied due to invalid credentials.");
            context.Result = new ObjectResult(result);
        }
    }
}
