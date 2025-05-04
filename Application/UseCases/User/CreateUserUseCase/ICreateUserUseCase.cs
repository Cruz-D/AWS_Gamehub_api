
using gamehub_API.Application.DTO.User;

namespace gamehub_API.Application.UseCases.User.CreateUserUseCase
{
    public interface ICreateUserUseCase
    {
        Task ExecuteAsync(CreateUserDTO createUserDTO);
    }
}
