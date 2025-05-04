using gamehub_API.Application.Interfaces;
using gamehub_API.Infrastructure.Models;

namespace gamehub_API.Application.UseCases.User.DeleteUserUseCase
{
    public class DeleteUserUseCase : IDeleteUserUseCase
    {
        private readonly IUserInterface _userInterface;

        public DeleteUserUseCase(IUserInterface userInterface)
        {
            _userInterface = userInterface;
        }

        public async Task<Users> ExecuteAsync(string userId)
        {
            var user = await _userInterface.GetUserByIdAsync(userId);

            return await _userInterface.DeleteUserAsync(user);
        }

    }

}
