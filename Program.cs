// ============================================
// Espacios de nombres necesarios
// ============================================
using Azure.Messaging.ServiceBus;
using gamehub_API.Application.Interfaces;
using gamehub_API.Application.UseCases.User.CreateUserUseCase;
using gamehub_API.Application.UseCases.User.DeleteUserUseCase;
using gamehub_API.Application.UseCases.User.EditUserUseCase;
using gamehub_API.Application.UseCases.User.LoginUserUseCase;
using gamehub_API.Application.UseCases.User.UpdatePasswordUseCase;
using gamehub_API.Application.UseCases.User.ViewUserUseCase;
using gamehub_API.Application.UseCases.Videogame.GetAllVideogamesUseCase;
using gamehub_API.Application.UseCases.Videogame.GetVideogameUseCase;
using gamehub_API.Infrastructure.Repositories;
using gamehub_API.Infrastructure.Services.ServiceBus;
using Microsoft.Azure.Cosmos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace gamehub_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Crear el builder para configurar la aplicación
            var builder = WebApplication.CreateBuilder(args);

            // ============================================
            // CONFIGURACIÓN DE SERVICIOS
            // ============================================

            // Configuración de autenticación con JWT
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                // Configuración de validación del token JWT
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true, // Validar el emisor del token
                    ValidateAudience = true, // Validar la audiencia del token
                    ValidateLifetime = true, // Validar la expiración del token
                    ValidateIssuerSigningKey = true, // Validar la clave de firma del token
                    ValidIssuer = builder.Configuration["Jwt:Issuer"], // Emisor válido desde appsettings.json
                    ValidAudience = builder.Configuration["Jwt:Audience"], // Audiencia válida desde appsettings.json
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder!.Configuration!["Jwt:Key"]!)) // Clave de firma
                };
            });

            // Agregar controladores al contenedor de servicios
            builder.Services.AddControllers();

            // Configuración de CORS para permitir solicitudes desde cualquier origen
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", policyBuilder =>
                {
                    policyBuilder.AllowAnyOrigin() // Permitir cualquier origen
                                 .AllowAnyMethod() // Permitir cualquier método HTTP
                                 .AllowAnyHeader(); // Permitir cualquier encabezado
                });
            });

            // Configuración de Swagger para la documentación de la API
            builder.Services.AddSwaggerGen(c =>
            {
                // Definición de seguridad para usar JWT en Swagger
                c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "Introduce el token JWT en el formato: Bearer {token}"
                });

                // Requisito de seguridad para todas las operaciones
                c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    {
                        new Microsoft.OpenApi.Models.OpenApiSecurityScheme 
                        {

                            Reference = new Microsoft.OpenApi.Models.OpenApiReference
                            {
                                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });

            // Configuración de Azure Service Bus como Singleton
            string serviceBusConnectionString = builder.Configuration!.GetValue<string>("ServiceBus:ConnectionString")!;
            builder.Services.AddSingleton(serviceProvider =>
            {
                return new ServiceBusClient(serviceBusConnectionString); // Crear instancia de ServiceBusClient
            });

            // Configuración de CosmosClient como Singleton
            builder.Services.AddSingleton(options =>
            {
                string endpointUri = builder.Configuration.GetSection("gamehub-cosmos")!.GetValue<string>("EndpointUri")!;
                string primaryKey = builder.Configuration.GetSection("gamehub-cosmos")!.GetValue<string>("PrimaryKey")!;

                // Crear una instancia de CosmosClient con las opciones de conexión
                return new CosmosClient(endpointUri, primaryKey, new CosmosClientOptions
                {
                    ConnectionMode = ConnectionMode.Gateway // Usar modo Gateway (HTTP/HTTPS)
                });
            });

            // ============================================
            // REGISTRO DE REPOSITORIOS
            // ============================================

            // Registrar el repositorio de videojuegos
            builder.Services.AddScoped<IVideogameInterface>(provider =>
            {
                var cosmosClient = provider.GetRequiredService<CosmosClient>();
                string databaseName = builder.Configuration.GetSection("gamehub-cosmos")!.GetValue<string>("DatabaseName")!;
                string containerName = builder.Configuration.GetSection("gamehub-cosmos")!.GetValue<string>("VideogamesContainer")!;
                var busServices = provider.GetRequiredService<BusServices>();

                return new VideogameRepository(cosmosClient, databaseName, containerName, busServices);
            });

            // Registrar el repositorio de usuarios
            builder.Services.AddScoped<IUserInterface>(provider =>
            {
                var cosmosClient = provider.GetRequiredService<CosmosClient>();
                string databaseName = builder.Configuration.GetSection("gamehub-cosmos")!.GetValue<string>("DatabaseName")!;
                string containerName = builder.Configuration.GetSection("gamehub-cosmos")!.GetValue<string>("UserContainer")!;
                var busServices = provider.GetRequiredService<BusServices>();

                return new UserRepository(cosmosClient, databaseName, containerName, busServices!);
            });

            // ============================================
            // REGISTRO DE CASOS DE USO
            // ============================================

            // Casos de uso para videojuegos
            builder.Services.AddScoped<IGetAllVideogamesUseCase, GetAllVideogamesUseCase>();
            builder.Services.AddScoped<IGetVideogameUseCase, GetVideogameUseCase>();

            // Casos de uso para usuarios
            builder.Services.AddScoped<ICreateUserUseCase, CreateUserUseCase>();
            builder.Services.AddScoped<ILoginUserUseCase, LoginUserUseCase>();
            builder.Services.AddScoped<IGetUserUseCase, GetUserUseCase>();
            builder.Services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
            builder.Services.AddScoped<IUpdatePasswordUserUseCase, UpdatePasswordUserUseCase>();
            builder.Services.AddScoped<IDeleteUserUseCase, DeleteUserUseCase>();

            // ============================================
            // REGISTRO DE SERVICIOS
            // ============================================

            builder.Services.AddScoped<BusServices>();
            builder.Services.AddScoped<IBusInterface, BusServices>();
            builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
            builder.Services.AddScoped<IIdGenerator, IdGenerator>();
            builder.Services.AddScoped<IJwtInterface, JwtService>();

            // Configuración de Swagger/OpenAPI
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // ============================================
            // CONFIGURACIÓN DE LA APLICACIÓN
            // ============================================

            var app = builder.Build();

            // Configuración del pipeline de solicitudes HTTP
            if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
            {
                app.UseSwagger(); // Habilitar Swagger
                app.UseSwaggerUI(); // Habilitar la interfaz de usuario de Swagger
            }

            app.UseHttpsRedirection(); // Redirigir a HTTPS
            app.UseAuthorization();   // Configurar autorización

            app.MapControllers(); // Mapear controladores

            app.Run(); // Ejecutar la aplicación
        }
    }
}
