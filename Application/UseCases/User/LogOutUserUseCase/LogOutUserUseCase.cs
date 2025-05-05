using gamehub_API.Application.DTO.User;
using gamehub_API.Application.Interfaces;

namespace gamehub_API.Application.UseCases.User.LogOutUserUseCase
{
    public class LogOutUserUseCase : ILogOutUserUseCase
    {
        private readonly IUserInterface _userInterface;

        public LogOutUserUseCase(IUserInterface userInterface)
        {
            _userInterface = userInterface;
        }

        public async Task ExecuteAsync(LogOutUserDTO logOutUserDTO)
        {
            try
            {
                var loggedUser = await _userInterface.GetUserByIdAsync(logOutUserDTO.userId);

                if (loggedUser == null)
                {
                    throw new Exception("Usuario no encontrado");
                }
                loggedUser.authentication!.isAuthenticated = false;
                loggedUser.authentication!.isLoggedIn = false;
                loggedUser.authentication!.accessToken = null;
                loggedUser.authentication!.tokenExpiry = null;
                loggedUser.authentication!.tokenCreatedAt = null;
                await _userInterface.UpdateUserAsync(loggedUser);
            }

            catch (Exception ex)
            {
                throw new Exception($"Error al cerrar sesión: {ex.Message}");
            }
        }

        // Método para verificar si un token está revocado
        public static bool IsTokenRevoked(string token)
        {
            return string.IsNullOrEmpty(token);
        }

    }
   
}
