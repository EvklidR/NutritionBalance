using UserProfileService.Application.Exceptions;
using Newtonsoft.Json;
using System.Net;

namespace UserProfileService.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode statusCode;
            object result;

            switch (exception)
            {
                case NotFoundException notFoundEx:
                    statusCode = HttpStatusCode.NotFound;
                    result = notFoundEx.Message;
                    break;
                case BadRequestException badRequestException:
                    statusCode = HttpStatusCode.BadRequest;
                    result = badRequestException.Errors;
                    break;
                case AlreadyExistsException alreadyExistsEx:
                    statusCode = HttpStatusCode.Conflict;
                    result = alreadyExistsEx.Message;
                    break;
                case UnauthorizedException unauthorizedEx:
                    statusCode = HttpStatusCode.Unauthorized;
                    result = unauthorizedEx.Message;
                    break;
                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    result = exception.Message;
                    break;
            }

            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";

            return context.Response.WriteAsync(JsonConvert.SerializeObject(result));
        }
    }
}
