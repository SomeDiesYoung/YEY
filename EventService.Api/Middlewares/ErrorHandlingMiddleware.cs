using EventManager.Domain.Exceptions;
using EventManager.Identity.Exceptions;
using EventManager.MessageSender.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace EventService.Api.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;
        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception: {Message}", ex.Message);
                await HandleExceptionAsync(httpContext, ex);
            }
        }
    


    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
            var problemDetails = new ProblemDetails
            {
                Title = "An unexpected error occurred",
                Status = (int)HttpStatusCode.InternalServerError,
                Instance = context.TraceIdentifier,
                Detail = exception.Message
            };

            switch (exception)
            {
                case System.UnauthorizedAccessException:
                    problemDetails.Status = (int)HttpStatusCode.Unauthorized;
                    problemDetails.Type = nameof(System.UnauthorizedAccessException);
                    problemDetails.Title = "Unauthorized access.";
                    break;
                case EventManager.Identity.Exceptions.UnauthorizedAccessException:
                    problemDetails.Title = exception.Message;
                    problemDetails.Status = (int)HttpStatusCode.Unauthorized;
                    problemDetails.Type = nameof(EventManager.Identity.Exceptions.UnauthorizedAccessException);
                    break;
                case ArgumentNullException:
                case ArgumentException:
                    problemDetails.Status = (int)HttpStatusCode.BadRequest;
                    problemDetails.Type = nameof(ArgumentException);
                    problemDetails.Title = "Invalid request";
                    break;
                case ChangePasswordException ex:
                    problemDetails.Status = (int)HttpStatusCode.BadRequest;
                    problemDetails.Type = nameof(ChangePasswordException);
                    problemDetails.Title = "Invalid request";
                    problemDetails.Extensions = ex.Errors?.ToDictionary(x => x.Code, object? (x) => x.Description) ??
                                          problemDetails.Extensions;
                    break;
                case IdentityException ex:
                    problemDetails.Status = (int)HttpStatusCode.BadRequest;
                    problemDetails.Type = nameof(IdentityException);
                    problemDetails.Title = "Invalid request";
                    problemDetails.Extensions = ex.Errors?.ToDictionary(x => x.Code, object? (x) => x.Description) ??
                                          problemDetails.Extensions;
                    break;
                case EmailSendingException:
                    problemDetails.Status = (int)HttpStatusCode.BadRequest;
                    problemDetails.Type = nameof(IdentityException);
                    problemDetails.Title = "Invalid request";
                    break;
                case KeyNotFoundException:
                    problemDetails.Status = (int)HttpStatusCode.NotFound;
                    problemDetails.Type = nameof(KeyNotFoundException);
                    problemDetails.Title = "Resource not found";
                    break;
                case ObjectNotFoundException:
                    problemDetails.Status = (int)HttpStatusCode.NotFound;
                    problemDetails.Type = nameof(ObjectNotFoundException);
                    problemDetails.Title = exception.Message;
                    break;
                case AlreadyExistsException:
                    problemDetails.Status = (int)HttpStatusCode.BadRequest;
                    problemDetails.Type = nameof(AlreadyExistsException);
                    problemDetails.Title = exception.Message;
                    break;
                case ValidationException:
                    problemDetails.Status = (int)HttpStatusCode.BadRequest;
                    problemDetails.Type = nameof(ValidationException);
                    problemDetails.Title = exception.Message;
                    break;
                case DomainException:
                    problemDetails.Status = (int)HttpStatusCode.BadRequest;
                    problemDetails.Type = nameof(DomainException);
                    problemDetails.Title = exception.Message;
                    break;
            }

            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = problemDetails.Status.Value;


            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var result = JsonSerializer.Serialize(problemDetails,options);
            return context.Response.WriteAsync(result);


    }
}

 
    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ErrorHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
