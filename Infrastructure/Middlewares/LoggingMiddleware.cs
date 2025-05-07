using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace gamehub_API.Infrastructure.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;

        /// <summary>
        /// Constructor del middleware que inyecta el delegado de la solicitud y el logger.
        /// </summary>
        /// <param name="next">El siguiente middleware en el pipeline.</param>
        /// <param name="logger">Instancia de ILogger para registrar logs.</param>
        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Método principal del middleware que intercepta las solicitudes HTTP.
        /// </summary>
        /// <param name="context">El contexto de la solicitud HTTP.</param>
        /// <returns>Una tarea que representa la ejecución del middleware.</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew(); // Inicia el temporizador para medir el tiempo de ejecución.

            try
            {
                // Log de la solicitud entrante.
                _logger.LogInformation("Solicitud entrante: {Method} {Path}", context.Request.Method, context.Request.Path);

                // Continúa con el siguiente middleware en el pipeline.
                await _next(context);

                // Log de la respuesta saliente.
                _logger.LogInformation("Respuesta saliente: {StatusCode} {Path} en {ElapsedMilliseconds}ms",
                    context.Response.StatusCode, context.Request.Path, stopwatch.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                // Log de la excepción si ocurre un error.
                _logger.LogError(ex, "Ocurrió un error al procesar la solicitud: {Method} {Path}", context.Request.Method, context.Request.Path);

                // Vuelve a lanzar la excepción para que sea manejada por el middleware de excepciones.
                throw;
            }
            finally
            {
                stopwatch.Stop(); // Detiene el temporizador.
            }
        }
    }
}
