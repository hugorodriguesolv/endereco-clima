using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace EnderecoClima.WebApi.ErrorHandling;

public sealed class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
      HttpContext httpContext,
      Exception exception,
      CancellationToken cancellationToken)
    {
        var (status, type, title, detail) = exception switch
        {
            // 400 - erros de requisição
            ArgumentException ex => (StatusCodes.Status400BadRequest, "invalid-request", "Invalid request", ex.Message),

            ValidationException ex => (StatusCodes.Status400BadRequest, "validation-error", "Validation error", ex.Message),

            // 404 - recurso inexistente
            KeyNotFoundException ex => (StatusCodes.Status404NotFound, "not-found", "Resource not found", ex.Message),

            // 409 - conflito de estado
            InvalidOperationException ex => (StatusCodes.Status409Conflict, "conflict", "Conflict", ex.Message),

            // 504 - timeout upstream
            TaskCanceledException ex => (StatusCodes.Status504GatewayTimeout, "upstream-timeout", "Upstream timeout", "The upstream service did not respond in time."),

            // 503 - falha de integração externa
            HttpRequestException ex => (StatusCodes.Status503ServiceUnavailable, "upstream-unavailable", "Service unavailable", ex.Message),

            // 500 - erro inesperado
            _ => (StatusCodes.Status500InternalServerError, "server-error", "Server error", "An unexpected error occurred.")
        };

        httpContext.Response.StatusCode = status;

        var problemDetails = new ProblemDetails
        {
            Status = status,
            Type = type,
            Title = title,
            Detail = detail,
            Instance = httpContext.Request.Path
        };

        problemDetails.Extensions["traceId"] = httpContext.TraceIdentifier;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}