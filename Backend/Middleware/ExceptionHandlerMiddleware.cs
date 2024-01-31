using System.Net;
using System.Text;

namespace pizzaria;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;

    public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
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

    //TODO: Handle unique id exception, set specific response
    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        string body = await GetRawBody(context);
        string endpoint = context.Request.Path;
        string method = context.Request.Method;

        _logger.LogError(exception, "{Time} | Error at {method} {endpoint}\n" +
                                    "Body: {body}", DateTime.Now, method, endpoint, body);

        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        await context.Response.CompleteAsync();
    }

    private async Task<string> GetRawBody(HttpContext context) {
        string body;
        try
        {
            context.Request.Body.Seek(0, SeekOrigin.Begin);
        }
        catch(Exception ex)
        {
            Console.WriteLine("Can't rewind body stream. " + ex.Message);
        }
        using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8))
            body = await reader.ReadToEndAsync();

        return body;
    }
}
