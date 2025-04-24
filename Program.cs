using gamehub_API.Application.Interfaces;
using gamehub_API.Application.UseCases.Videogame.GetAllVideogamesUseCase;
using gamehub_API.DbContext.NewFolder;
using gamehub_API.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace gamehub_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ============================================
            // CONFIGURACIÓN DE SERVICIOS
            // ============================================

            // Agregar controladores al contenedor
            builder.Services.AddControllers();

            // Configurar el contexto de base de datos (LocalDbContext)
            builder.Services.AddDbContext<LocalDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("AzureSQLConnection"))); // AzureSQLConnection LocalDbConnection

            // Configurar política de CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", policyBuilder =>
                {
                    policyBuilder.AllowAnyOrigin()
                                 .AllowAnyMethod()
                                 .AllowAnyHeader();
                });
            });

            // Registrar repositorios
            builder.Services.AddScoped<IVideogameRepository, VideogameRepository>();

            // Registrar casos de uso
            builder.Services.AddScoped<IGetAllVideogamesUseCase, GetAllVideogamesUseCase>();

            // Configurar Swagger/OpenAPI
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // ============================================
            // CONFIGURACIÓN DE LA APLICACIÓN
            // ============================================

            var app = builder.Build();

            // Configurar el pipeline de solicitudes HTTP
            if (app.Environment.IsDevelopment())
            {
                // Habilitar Swagger en entorno de desarrollo
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection(); // Redirección a HTTPS
            app.UseAuthorization();   // Configuración de autorización

            // Mapear controladores
            app.MapControllers();

            // Ejecutar la aplicación
            app.Run();
        }
    }
}
