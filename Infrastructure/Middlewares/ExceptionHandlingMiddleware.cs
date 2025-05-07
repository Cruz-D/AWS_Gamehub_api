public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    /// <summary>
    /// Constructor del middleware que recibe el siguiente delegado en el pipeline de solicitudes.
    /// </summary>
    /// <param name="next">El siguiente middleware en el pipeline.</param>
    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    /// <summary>
    /// Método principal del middleware que intercepta las solicitudes HTTP.
    /// </summary>
    /// <param name="context">El contexto de la solicitud HTTP.</param>
    /// <returns>Una tarea que representa la ejecución del middleware.</returns>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            // Intenta ejecutar el siguiente middleware en el pipeline.
            await _next(context);
        }
        catch (Exception ex)
        {
            // Si ocurre una excepción, maneja el error llamando a HandleExceptionAsync.
            await HandleExceptionAsync(context, ex);
        }
    }

    /// <summary>
    /// Maneja las excepciones capturadas y genera una respuesta HTTP adecuada.
    /// </summary>
    /// <param name="context">El contexto de la solicitud HTTP.</param>
    /// <param name="exception">La excepción que se produjo.</param>
    /// <returns>Una tarea que representa la escritura de la respuesta HTTP.</returns>
    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        // Configura el tipo de contenido de la respuesta como JSON.
        context.Response.ContentType = "application/json";

        // Determina el código de estado HTTP basado en el tipo de excepción.
        context.Response.StatusCode = exception switch
        {
            ArgumentException => StatusCodes.Status400BadRequest, // Error de solicitud incorrecta.
            KeyNotFoundException => StatusCodes.Status404NotFound, // Recurso no encontrado.
            _ => StatusCodes.Status500InternalServerError // Error interno del servidor para excepciones no manejadas.
        };

        // Crea un objeto de respuesta con un mensaje y detalles.
        var response = new
        {
            message = exception.Message, // Mensaje de la excepción.
            details = context.Response.StatusCode == StatusCodes.Status500InternalServerError
                ? "Error interno del servidor." // Mensaje genérico para errores internos.
                : exception.Message // Mensaje específico para otros errores.
        };

        // Escribe la respuesta como JSON en el cuerpo de la respuesta HTTP.
        return context.Response.WriteAsJsonAsync(response);
    }
}
