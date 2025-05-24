// ============================================
// Espacios de nombres necesarios
// ============================================
using gamehub_API.Application.UseCases.User.CreateUserUseCase;
using gamehub_API.Application.UseCases.User.DeleteUserUseCase;
using gamehub_API.Application.UseCases.User.EditUserUseCase;
using gamehub_API.Application.UseCases.User.UpdatePasswordUseCase;
using gamehub_API.Application.UseCases.User.ViewUserUseCase;
using gamehub_API.Application.UseCases.Videogame.GetAllVideogamesUseCase;
using gamehub_API.Application.UseCases.Videogame.GetVideogameUseCase;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using gamehub_API.Infrastructure.Middlewares;
using gamehub_API.Application.UseCases.Auth.LogOutUserUseCase;
using gamehub_API.Application.UseCases.Auth.LoginUserUseCase;
using gamehub_API.Application.UseCases.Auth.RefreshTokenUserUseCase;
using gamehub_API.Application.Interfaces.Others;
using gamehub_API.Application.Interfaces;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using gamehub_API.Infrastructure.Repositories;
using gamehub_API.Application.UseCases.Comments.ReadCommentsUseCase;
using gamehub_API.Application.UseCases.Comments.CreateCommentUseCase;
using gamehub_API.Application.UseCases.Comments.DeleteCommentUseCase;
using gamehub_API.Application.UseCases.Comments.EditCommentUseCase;

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

            builder.Services.AddSingleton<IAmazonDynamoDB>(sp =>
            {
                var config = sp.GetRequiredService<IConfiguration>();
                var region = Amazon.RegionEndpoint.EUNorth1.SystemName; // Cambia esto a la región deseada
                return new AmazonDynamoDBClient(Amazon.RegionEndpoint.GetBySystemName(region));
            });

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
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder!.Configuration!["Jwt:Key"]!)), // Clave de firma
                    //validar el tiempo de expiración del token
                    LifetimeValidator = (notBefore, expires, securityToken, validationParameters) =>
                    {
                        var jwtToken = securityToken as JwtSecurityToken;
                        return expires > DateTime.UtcNow || !LogOutUserUseCase.IsTokenRevoked(jwtToken?.RawData ?? string.Empty);
                    }
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

           
            // Registrar el contexto de DynamoDB
            builder.Services.AddSingleton<IDynamoDBContext, DynamoDBContext>();




            // ============================================
            // REGISTRO DE REPOSITORIOS
            // ============================================
            builder.Services.AddScoped<IVideogameInterface, VideogameRepository>();

            //Registrar el repositorio de videojuegos
            //builder.Services.AddScoped<IVideogameInterface>(provider =>
            //{
            //    var cosmosClient = provider.GetRequiredService<CosmosClient>();
            //    string databaseName = builder.Configuration.GetSection("gamehub-cosmos")!.GetValue<string>("DatabaseName")!;
            //    string containerName = builder.Configuration.GetSection("gamehub-cosmos")!.GetValue<string>("VideogamesContainer")!;

            //    return new VideogameRepository(cosmosClient, databaseName, containerName, busServices);
            //});

            //Registrar el repositorio de videojuegos
            
           

            // With this corrected code block:  
            builder.Services.AddScoped<IUserInterface>(provider =>
            {
                var client = provider.GetRequiredService<IAmazonDynamoDB>();
                var dynamoDBContext = new DynamoDBContext(client);
                return new UserRepository(dynamoDBContext, client);
            });
             
            builder.Services.AddScoped<IAuthInterface>(provider =>
            {
                var client = new AmazonDynamoDBClient();
                var dynamoDBContext = new DynamoDBContext(client);
                var context = new UserRepository(dynamoDBContext, client);          
                return new AuthRepository(dynamoDBContext, client);
            });

            builder.Services.AddScoped<ICommentsInterface>(provider =>
            {
                var client = provider.GetRequiredService<IAmazonDynamoDB>();
                var dynamoDBContext = new DynamoDBContext(client);
                return new CommentRepository(dynamoDBContext, client);
            });

            // ============================================
            // REGISTRO DE CASOS DE USO
            // ============================================

            // Caso de uso para Auth

            // Casos de uso para videojuegos
            builder.Services.AddScoped<IGetAllVideogamesUseCase, GetAllVideogamesUseCase>();
            builder.Services.AddScoped<IGetVideogameUseCase, GetVideogameUseCase>();

            // Casos de uso para autenticación
            builder.Services.AddScoped<ILoginUserUseCase, LoginUserUseCase>();
            builder.Services.AddScoped<ILogOutUserUseCase, LogOutUserUseCase>();
            builder.Services.AddScoped<IRefreshTokenUserUseCase, RefreshTokenUseCase>();


            // Casos de uso para usuarios
            builder.Services.AddScoped<ICreateUserUseCase, CreateUserUseCase>();
            builder.Services.AddScoped<IGetUserUseCase, GetUserUseCase>();
            builder.Services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
            builder.Services.AddScoped<IUpdatePasswordUserUseCase, UpdatePasswordUserUseCase>();
            builder.Services.AddScoped<IDeleteUserUseCase, DeleteUserUseCase>();

            // Casos de uso para los comentarios
            builder.Services.AddScoped<IReadCommentUseCase, ReadCommentUseCase>();
            builder.Services.AddScoped<ICreateCommentUseCase, CreateCommentUseCase>();
            builder.Services.AddScoped<IEditCommentUseCase, EditCommentUseCase>();
            builder.Services.AddScoped<IDeleteCommentUseCase, DeleteCommentUseCase>();

            // ============================================
            // REGISTRO DE SERVICIOS
            // ============================================

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

            app.UseCors("AllowAllOrigins"); // Aplicar la política de CORS

            // middleware para obtener logs
            app.UseMiddleware<LoggingMiddleware>();

            // Configuración del pipeline de solicitudes HTTP
            if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
            {
                app.UseSwagger(); // Habilitar Swagger
                app.UseSwaggerUI(); // Habilitar la interfaz de usuario de Swagger
            }

            // Middleware global para manejo de excepciones
            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseHttpsRedirection(); // Redirigir a HTTPS
            app.UseAuthorization();   // Configurar autorización

            app.MapControllers(); // Mapear controladores

            app.Run(); // Ejecutar la aplicación
        }
    }
}
