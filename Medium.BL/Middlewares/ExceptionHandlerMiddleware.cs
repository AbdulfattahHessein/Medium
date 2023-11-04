using FluentValidation;
using Medium.BL.ResponseHandler;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;
using System.Net.Mime;

namespace Medium.BL.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        async public Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }

            catch (Exception exception)
            {
                var httpStatusCode = ConfigurateExceptionTypes(exception);

                context.Response.StatusCode = httpStatusCode;
                context.Response.ContentType = MediaTypeNames.Application.Json; //"application/json"

                if (exception is ValidationException ex)
                {
                    var responseResult = new ApiResponse() { StatusCode = (HttpStatusCode)httpStatusCode, Message = ex.Message };


                    var errors = new Dictionary<string, List<string>>();
                    foreach (var error in ex.Errors)
                    {
                        if (errors.ContainsKey(error.PropertyName))
                            errors[error.PropertyName].Add(error.ErrorMessage);
                        else errors.Add(error.PropertyName, new List<string>() { error.ErrorMessage });

                    }
                    responseResult.Errors = errors;
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(responseResult));
                }
                else
                {
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(new { exception.Message }));
                }

            }
        }

        private static int ConfigurateExceptionTypes(Exception exception)
        {
            int httpStatusCode = exception switch
            {
                var _ when exception is ValidationException => (int)HttpStatusCode.UnprocessableEntity,
                _ => (int)HttpStatusCode.InternalServerError

            };

            return httpStatusCode;
        }
    }
}
