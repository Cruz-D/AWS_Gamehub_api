
using gamehub_API.Application.Interfaces;

namespace gamehub_API.Application.UseCases.User.LoginUserUseCase
{
    public class LoginUserUseCase : ILoginUserUseCase
    {
        private readonly IUserInterface _userInterface;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IIdGenerator _idGenerator;

        public LoginUserUseCase(IUserInterface userInterface, IPasswordHasher passwordHasher, IIdGenerator idGenerator)
        {
            _userInterface = userInterface;
            _passwordHasher = passwordHasher;
            _idGenerator = idGenerator;
        }

        public async Task<LoginResponseUserDTO> ExecuteAsync(LoginUserDTO loginUserDTO)
        {
            try
            {
                var loggedUser = await _userInterface.LoginUserAsync(loginUserDTO.usernameOrEmail, loginUserDTO.password);

                // Verificar la contraseña  
                var isPasswordValid = _passwordHasher.VerifyPassword(loginUserDTO.password, loggedUser.systemInfo.password);


                if (!isPasswordValid)
                {
                    throw new Exception("Usuario o contraseña incorrectos");
                }

                loggedUser.authentication.isAuthenticated = true; // 
                loggedUser.authentication.isLoggedIn = true;
                loggedUser.timestamps.lastLogin = DateTime.UtcNow.ToString("o");

                var updatedUser = await _userInterface.UpdateUserAsync(loggedUser);

                // Mapear los resultados de vuelta a LoginResponseUserDTO  
                var loginResponde = new LoginResponseUserDTO
                {
                    UserId = loggedUser.userId,
                    Username = loggedUser.systemInfo.username,
                    Email = loggedUser.systemInfo.email,
                    LastLogin = loggedUser.timestamps.lastLogin,
                    IsAuthenticated = isPasswordValid
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
