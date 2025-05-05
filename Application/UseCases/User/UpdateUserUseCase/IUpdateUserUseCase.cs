using gamehub_API.Infrastructure.Models;

namespace gamehub_API.Application.UseCases.User.EditUserUseCase
{
    public interface IUpdateUserUseCase
    {
        Task<GetUserDTO> ExecuteAsync(UpdateUserDTO updateUserDTO);
    }
}
