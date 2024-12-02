﻿using MealPlanService.Application.Exceptions;
using Newtonsoft.Json;
using System.Net;

namespace MealPlanService.API.Middleware
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
                case AlreadyExistsException alreadyExistsException:
                    statusCode = HttpStatusCode.Conflict;
                    result = alreadyExistsException.Message;
                    break;
                case BadRequestException badRequestException:
                    statusCode = HttpStatusCode.BadRequest;
                    result = badRequestException.Errors;
                    break;
                case NotFoundException notFoundEx:
                    statusCode = HttpStatusCode.NotFound;
                    result = notFoundEx.Message;
                    break;
                case BusinessLogicException businessLogicEx:
                    statusCode = HttpStatusCode.Conflict;
                    result = businessLogicEx.Message;
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
