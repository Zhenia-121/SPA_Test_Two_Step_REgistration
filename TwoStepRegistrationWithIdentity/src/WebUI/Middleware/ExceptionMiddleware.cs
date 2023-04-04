using System.Net;
using Application.Common.Exceptions;
using Microsoft.IdentityModel.Tokens;
using TwoStepRegistrationWithIdentity.Models;
using static System.String;

namespace TwoStepRegistrationWithIdentity.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try { await _next(context); }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            _logger.LogError(ex.ToString());

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)GetStatusCode(ex);

            await context.Response.WriteAsync(
                new ApiErrorModel
                {
                    Code = context.Response.StatusCode,
                    Message = context.Response.StatusCode == (int)HttpStatusCode.InternalServerError ? "Error.ServerInternal" : ex.Message,
                    Description = ex is ValidationException ? ex.ToString() : Empty
                }.ToString()
            );
        }
    }

    private static HttpStatusCode GetStatusCode(Exception ex)
    {
        return ex switch
        {
            ValidationException => HttpStatusCode.BadRequest,
            SecurityTokenException => HttpStatusCode.Unauthorized,
            _ => HttpStatusCode.InternalServerError,
        };
    }
}
