using gamehub_API.Infrastructure.Models;

namespace gamehub_API.Application.Interfaces
{
    public interface IUserInterface
    {
        Task<Users> GetUserByIdAsync(string userId);
        Task<Users> AddUserAsync(Users user);
        Task<Users> UpdateUserAsync(Users user);
        Task<Users> UpdatePasswordUserAsync(Users user);
        Task<Users> DeleteUserAsync(Users user);
    }
}
