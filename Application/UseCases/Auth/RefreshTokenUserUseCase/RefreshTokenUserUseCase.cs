using gamehub_API.Application.DTO.Auth;
using gamehub_API.Application.Interfaces;
using gamehub_API.Application.Interfaces.Others;

namespace gamehub_API.Application.UseCases.Auth.RefreshTokenUserUseCase
{
    public class RefreshTokenUseCase : IRefreshTokenUserUseCase
    {
        private readonly IUserInterface _userInterface;
        private readonly IJwtInterface _jwtInterface;

        public RefreshTokenUseCase(IUserInterface userInterface, IJwtInterface jwtInterface)
        {
            _userInterface = userInterface;
            _jwtInterface = jwtInterface;
        }

        public async Task<LoginResponseUserDTO> ExecuteAsync(RefreshTokenDTO refreshTokenDTO)
        {
            // Validar entrada
            if (string.IsNullOrEmpty(refreshTokenDTO.UserId) || string.IsNullOrEmpty(refreshTokenDTO.RefreshToken))
                throw new ArgumentException("Datos de refresh token inválidos.");

            // Obtener el usuario
            var user = await _userInterface.GetUserByIdAsync(refreshTokenDTO.UserId);
            if (user == null || user.authentication == null)
                throw new KeyNotFoundException("Usuario no encontrado.");

            // Validar el refresh token
            if (user.authentication.refreshToken != refreshTokenDTO.RefreshToken ||
                DateTime.UtcNow > DateTime.Parse(user.authentication.tokenExpiry))
            {
                throw new UnauthorizedAccessException("Refresh token inválido o expirado.");
            }

            // Generar un nuevo access token
            var newAccessToken = _jwtInterface.GenerateToken(user.userId, user.systemInfo.username, user.systemInfo.role, "access");

            // Actualizar el usuario con el nuevo access token
            user.authentication.accessToken = newAccessToken;
            user.authentication.tokenCreatedAt = DateTime.UtcNow.ToString("o");
            user.authentication.tokenExpiry = DateTime.UtcNow.AddHours(1).ToString("o");

            await _userInterface.UpdateUserAsync(user);

            // Retornar la respuesta
            return new LoginResponseUserDTO
            {
                userId = user.userId,
                IsAuthenticated = true,
                accessToken = newAccessToken,
                refreshToken = user.authentication.refreshToken,
                tokenCreatedAt = user.authentication.tokenCreatedAt,
                tokenExpiry = user.authentication.tokenExpiry,
                LastLogin = user.timestamps.lastLogin
            };
        }
    }
}
