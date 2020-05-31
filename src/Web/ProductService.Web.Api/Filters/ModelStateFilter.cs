using System;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ProductService.Web.Api.Models;

namespace ProductService.Web.Api.Filters
{
    public sealed class ModelStateFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid)
                return;

            var result = Result.Failure(HttpStatusCode.BadRequest, GetErrorMessage(context.ModelState));
            context.Result = new ObjectResult(result);
        }

        private static string GetErrorMessage(ModelStateDictionary modelState)
        {
            var errorMessages = modelState.Values
                .SelectMany(x => x.Errors)
                .Select(x => x.ErrorMessage);

            return String.Join(" ", errorMessages);
        }
    }
}
