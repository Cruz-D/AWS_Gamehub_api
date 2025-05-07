using gamehub_API.Application.DTO.Auth;

namespace gamehub_API.Application.UseCases.Auth.RefreshTokenUserUseCase
{
    public interface IRefreshTokenUserUseCase
    {
        Task<LoginResponseUserDTO> ExecuteAsync(RefreshTokenDTO refreshTokenDTO);
    }
}
