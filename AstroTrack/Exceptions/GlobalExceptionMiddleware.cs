using System.Net;
using System.Text.Json;
using AstroTrack.DTOs.Responses;

namespace AstroTrack.Exceptions;

public class GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Exceção não tratada: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var (statusCode, erro) = exception switch
        {
            NotFoundException => (HttpStatusCode.NotFound, "Recurso não encontrado"),
            BadRequestException => (HttpStatusCode.BadRequest, "Requisição inválida"),
            ConflictException => (HttpStatusCode.Conflict, "Conflito de dados"),
            UnauthorizedException => (HttpStatusCode.Unauthorized, "Não autorizado"),
            _ => (HttpStatusCode.InternalServerError, "Erro interno do servidor")
        };

        var response = new ErroResponse(
            Timestamp: DateTime.UtcNow,
            Status: (int)statusCode,
            Erro: erro,
            Mensagem: exception.Message,
            Caminho: context.Request.Path,
            Campos: null
        );

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        await context.Response.WriteAsync(json);
    }
}