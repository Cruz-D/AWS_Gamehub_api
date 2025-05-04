using gamehub_API.Infrastructure.Models;

namespace gamehub_API.Application.UseCases.User.DeleteUserUseCase
{
    public interface IDeleteUserUseCase
    {
        Task<Users> ExecuteAsync(string userId);
    }
}
