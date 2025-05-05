using gamehub_API.Infrastructure.Models;

namespace gamehub_API.Application.UseCases.User.ViewUserUseCase
{
    public interface IGetUserUseCase
    {
        Task<GetUserDTO> ExecuteAsync(string userId);
    }
}
