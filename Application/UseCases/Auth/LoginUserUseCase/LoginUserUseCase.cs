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

        public LoginUserUseCase(IUserInterface userInterface, IPasswordHasher passwordHasher, IJwtInterface jwtInterface, IAuthInterface authInterface )
        {
            _userInterface = userInterface;
            _passwordHasher = passwordHasher;
            _jwtInterface = jwtInterface;
            _authInterface = authInterface;
        }

        public async Task<LoginResponseUserDTO> ExecuteAsync(LoginUserDTO loginUserDTO)
        {
            if (loginUserDTO.usernameOrEmail == "" || loginUserDTO.password == "")
                throw new ArgumentNullException(nameof(loginUserDTO), "Los datos de inicio de sesión no pueden ser nulos.");

            try
            {
                // Obtener el usuario por nombre de usuario o correo electrónico
                var loggedUser = await _authInterface.LoginUserAsync(loginUserDTO.usernameOrEmail, loginUserDTO.password);

                if (loggedUser == null)
                    throw new KeyNotFoundException("Usuario no encontrado.");

                // Verificar la contraseña con la biblioteca de BCrypt
                var isPasswordValid = _passwordHasher.VerifyPassword(loginUserDTO.password, loggedUser.systemInfo.password);

                if (!isPasswordValid)
                    throw new UnauthorizedAccessException("Usuario o contraseña incorrectos.");

                // Añadir los tokens
                var accessToken = _jwtInterface.GenerateToken(loggedUser.userId, loggedUser.systemInfo.username, loggedUser.systemInfo.role, "refresh");
                var refreshToken = _jwtInterface.GenerateRefreshToken();

                if (string.IsNullOrEmpty(accessToken))
                    throw new InvalidOperationException("Error al generar el token de acceso.");

                // Actualizar el usuario con los nuevos tokens y la fecha de inicio de sesión
                loggedUser.authentication.isAuthenticated = true;
                loggedUser.authentication.isLoggedIn = true;
                loggedUser.timestamps.lastLogin = DateTime.UtcNow.ToString("o");
                loggedUser.authentication.accessToken = accessToken;
                loggedUser.authentication.refreshToken = refreshToken;
                loggedUser.authentication.tokenExpiry = DateTime.UtcNow.AddHours(1).ToString("o");
                loggedUser.authentication.tokenCreatedAt = DateTime.UtcNow.ToString("o");

                // Actualizar el usuario en la base de datos
                var updatedUser = await _userInterface.UpdateUserAsync(loggedUser);

                if (updatedUser == null)
                    throw new InvalidOperationException("Error al actualizar el usuario.");

                // Crear la respuesta de inicio de sesión
                return new LoginResponseUserDTO
                {
                    userId = updatedUser.userId,
                    LastLogin = loggedUser.timestamps.lastLogin,
                    IsAuthenticated = isPasswordValid,
                    accessToken = updatedUser.authentication.accessToken,
                    refreshToken = updatedUser.authentication.refreshToken,
                    tokenCreatedAt = updatedUser.timestamps.createdAt,
                    tokenExpiry = updatedUser.authentication.tokenExpiry,
                };
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
