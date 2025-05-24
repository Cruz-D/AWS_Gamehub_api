using gamehub_API.Application.Interfaces;
using gamehub_API.Infrastructure.Models;

namespace gamehub_API.Application.UseCases.User.ViewUserUseCase
{
    public class GetUserUseCase : IGetUserUseCase
    {
        private readonly IUserInterface _userInterface;

        public GetUserUseCase(IUserInterface userInterface)
        {
            _userInterface = userInterface;
        }

        public async Task<GetUserDTO> ExecuteAsync(string userId)
        {
            var getUser = await _userInterface.GetUserByIdAsync(userId);

            return new GetUserDTO
            {
                username = getUser.systemInfo.username,
                email = getUser.systemInfo.email,
                firstName = getUser.personalInfo.firstName,
                lastName = getUser.personalInfo.lastName,
                dateOfBirth = getUser.personalInfo.dateOfBirth,
                role = getUser.systemInfo.role,
                //createdAt = DateTime.UtcNow.ToString("o"), // Example value  
                //lastLogin = null // Example value  
            };
        }
    }
}
