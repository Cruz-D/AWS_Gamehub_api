using gamehub_API.Application.DTO.Auth;

namespace gamehub_API.Application.UseCases.Auth.LogOutUserUseCase
{
    public interface ILogOutUserUseCase
    {

        Task ExecuteAsync(LogOutUserDTO logOutUserDTO);

    }
}
