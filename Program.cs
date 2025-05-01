using gamehub_API.Application.Interfaces;
using gamehub_API.Application.UseCases.Videogame.GetAllVideogamesUseCase;
using gamehub_API.Application.UseCases.Videogame.GetVideogameUseCase;
using gamehub_API.Infrastructure.Repositories;
using Microsoft.Azure.Cosmos;

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

            // Configurar CosmosClient como Singleton
            builder.Services.AddSingleton(options =>
            {
                string endpointUri = builder.Configuration.GetSection("gamehub-cosmos")!.GetValue<string>("EndpointUri")!;
                string primaryKey = builder.Configuration.GetSection("gamehub-cosmos")!.GetValue<string>("PrimaryKey")!;

                // Crear una instancia de CosmosClient con las opciones de conexión
                return new CosmosClient(endpointUri, primaryKey, new CosmosClientOptions
                {
                    // Configurar el modo de conexión a la base de datos a Gateway
                    // es decir, a través de HTTP o HTTPS en lugar de TCP 
                    ConnectionMode = ConnectionMode.Gateway
                });
            });

            //---------------------------------------------
            // Registrar Repositorios
            //---------------------------------------------
            builder.Services.AddScoped<IVideogameRepository>(provider =>
            {
                // Obtener el cosmosClient generado anteriormente
                var cosmosClient = provider.GetRequiredService<CosmosClient>();
                string databaseName = builder.Configuration.GetSection("gamehub-cosmos")!.GetValue<string>("DatabaseName")!;
                string containerName = builder.Configuration.GetSection("gamehub-cosmos")!.GetValue<string>("VideogamesContainer")!;

                return new VideogameRepository(cosmosClient, databaseName, containerName);
            });

            //---------------------------------------------
            // Registrar casos de uso
            //---------------------------------------------
            builder.Services.AddScoped<IGetAllVideogamesUseCase, GetAllVideogamesUseCase>();
            builder.Services.AddScoped<IGetVideogameUseCase, GetVideogameUseCase>();

            // Configurar Swagger/OpenAPI
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // ============================================
            // CONFIGURACIÓN DE LA APLICACIÓN
            // ============================================

            var app = builder.Build();

            // Configurar el pipeline de solicitudes HTTP
            if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
            {
                // Habilitar Swagger en desarrollo y producción
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
