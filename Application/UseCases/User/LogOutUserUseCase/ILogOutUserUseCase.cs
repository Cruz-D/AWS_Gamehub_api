using gamehub_API.Application.DTO.User;

namespace gamehub_API.Application.UseCases.User.LogOutUserUseCase
{
    public interface ILogOutUserUseCase
    {
        
        Task ExecuteAsync(LogOutUserDTO logOutUserDTO);

    }
}
