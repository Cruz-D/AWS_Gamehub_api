using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using gamehub_API.Application.Interfaces;
using gamehub_API.Application.Interfaces.Others;

namespace gamehub_API.Application.UseCases.Auth.LoginUserUseCase
{
    public class LoginUserUseCase : ILoginUserUseCase
    {
        private readonly IUserInterface _userInterface;
        private readonly IAuthInterface _authInterface;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtInterface _jwtInterface;
        private readonly IDynamoDBContext _dynamoDBContext;
        private readonly IAmazonDynamoDB _dynamoDBClient;

        public LoginUserUseCase(
            IUserInterface userInterface, 
            IPasswordHasher passwordHasher, 
            IJwtInterface jwtInterface, 
            IAuthInterface authInterface, 
            IDynamoDBContext dynamoDBContext,
            IAmazonDynamoDB amazonDynamoDB)
        {
            _userInterface = userInterface;
            _passwordHasher = passwordHasher;
            _jwtInterface = jwtInterface;
            _authInterface = authInterface;
            _dynamoDBContext = dynamoDBContext;
            _dynamoDBClient = amazonDynamoDB;
        }

        public async Task<LoginResponseUserDTO> ExecuteAsync(LoginUserDTO loginUserDTO)
        {
            if (loginUserDTO.usernameOrEmail == "" || loginUserDTO.password == "")
                throw new ArgumentNullException(nameof(loginUserDTO), "Los datos de inicio de sesión no pueden ser nulos.");

            try
            {
                // Obtener el usuario por nombre de usuario o correo electrónico
                var loggedUser = await _authInterface.GetUserByUsernameOrEmailAsync(loginUserDTO.usernameOrEmail);

                if (loggedUser == null)
                    throw new KeyNotFoundException("Usuario no encontrado.");

                // Verificar la contraseña con la biblioteca de BCrypt
                var isPasswordValid = _passwordHasher.VerifyPassword(loginUserDTO.password, loggedUser.systemInfo.password);

                if (!isPasswordValid)
                    throw new UnauthorizedAccessException("Usuario o contraseña incorrectos.");

                // Añadir los tokens
                var accessToken = _jwtInterface.GenerateToken(loggedUser.userId, loggedUser.systemInfo.username, loggedUser.systemInfo.email);
                var refreshToken = _jwtInterface.GenerateRefreshToken();

                if (string.IsNullOrEmpty(accessToken))
                    throw new InvalidOperationException("Error al generar el token de acceso.");

                // Actualizar el usuario en la base de datos
                var updateRequest = new UpdateItemRequest
                {
                    TableName = "Users",
                    Key = new Dictionary<string, AttributeValue>
                    {
                        { "userId", new AttributeValue { S = loggedUser.userId } }
                    },
                    UpdateExpression = "SET authentication.accessToken = :accessToken, " +
                       "authentication.refreshToken = :refreshToken, " +
                       "authentication.tokenCreatedAt = :tokenCreatedAt, " +
                       "authentication.tokenExpiry = :tokenExpiry, " +
                       "timestamps.lastLogin = :lastLogin",

                    ExpressionAttributeValues = new Dictionary<string, AttributeValue>
                    {
                        { ":accessToken", new AttributeValue { S = accessToken } },
                        { ":refreshToken", new AttributeValue { S = refreshToken } },
                        { ":tokenCreatedAt", new AttributeValue { S = DateTime.UtcNow.ToString("o") } },
                        { ":tokenExpiry", new AttributeValue { S = DateTime.UtcNow.AddHours(1).ToString("o") } },
                        { ":lastLogin", new AttributeValue { S = DateTime.UtcNow.ToString("o") } }
                    }
                };

                var updateResponse = await _dynamoDBClient.UpdateItemAsync(updateRequest);

                if (updateResponse.HttpStatusCode != System.Net.HttpStatusCode.OK)
                    throw new InvalidOperationException("Error al actualizar el usuario en la base de datos.");

                // Mapear la respuesta a un DTO
                var loginResponseUserDTO = new LoginResponseUserDTO
                {
                    userId = loggedUser.userId,
                    accessToken = accessToken,
                    refreshToken = refreshToken,
                    LastLogin = loggedUser.timestamps.lastLogin,
                    tokenCreatedAt = loggedUser.authentication.tokenCreatedAt,
                    tokenExpiry = loggedUser.authentication.tokenExpiry,

                };

                // Devolver la respuesta
                return loginResponseUserDTO;


            }

            catch (UnauthorizedAccessException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al iniciar sesión.", ex);
            }
        }

    }
}
