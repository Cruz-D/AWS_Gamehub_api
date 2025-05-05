
using gamehub_API.Application.Interfaces;

namespace gamehub_API.Application.UseCases.User.LoginUserUseCase
{
    public class LoginUserUseCase : ILoginUserUseCase
    {
        private readonly IUserInterface _userInterface;
        private readonly IPasswordHasher _passwordHasher;
        
        private readonly IJwtInterface _jwtInterface;

        public LoginUserUseCase(IUserInterface userInterface, IPasswordHasher passwordHasher, IJwtInterface jwtInterface)
        {
            _userInterface = userInterface;
            _passwordHasher = passwordHasher;
            _jwtInterface = jwtInterface;
        }

        public async Task<LoginResponseUserDTO> ExecuteAsync(LoginUserDTO loginUserDTO)
        {
            try
            {
                var loggedUser = await _userInterface.LoginUserAsync(loginUserDTO!.usernameOrEmail!, loginUserDTO!.password!);

                // Verificar la contraseña  
                var isPasswordValid = _passwordHasher.VerifyPassword(loginUserDTO!.password!, loggedUser!.systemInfo!.password!);


                if (!isPasswordValid)
                {
                    throw new Exception("Usuario o contraseña incorrectos");
                }

                //generar un nuevo token de acceso
                var accessToken = _jwtInterface.GenerateToken(loggedUser!.userId!, loggedUser!.systemInfo!.username!, loggedUser!.systemInfo!.role!, "refresh");

                //generar refresh token
                var refreshToken = _jwtInterface.GenerateRefreshToken();

                if (accessToken == null)
                {
                    throw new Exception("Error al generar el token de acceso");
                }

                loggedUser!.authentication!.isAuthenticated = true; // 
                loggedUser!.authentication!.isLoggedIn = true;
                loggedUser!.timestamps!.lastLogin = DateTime.UtcNow.ToString("o");
                loggedUser!.authentication!.accessToken = accessToken;
                loggedUser!.authentication!.refreshToken = refreshToken;
                loggedUser!.authentication!.tokenExpiry = DateTime.UtcNow.AddHours(1).ToString("o");
                loggedUser!.authentication!.tokenCreatedAt = DateTime.UtcNow.ToString("o");

                var updatedUser = await _userInterface.UpdateUserAsync(loggedUser);

                // Mapear los resultados de vuelta a LoginResponseUserDTO  
                var loginResponde = new LoginResponseUserDTO
                {
                    userId = updatedUser!.userId,
                    LastLogin = loggedUser!.timestamps!.lastLogin,
                    IsAuthenticated = isPasswordValid,
                    accessToken = updatedUser!.authentication!.accessToken,
                    refreshToken = updatedUser!.authentication!.refreshToken,
                    tokenCreatedAt = updatedUser!.timestamps!.createdAt,
                    tokenExpiry = updatedUser!.authentication!.tokenExpiry,

                };

                return loginResponde;

            }
            catch (Exception ex)
            {
                // Manejo de excepciones  
                throw new Exception("Error al iniciar sesión", ex);
            }
        }
    }
}
