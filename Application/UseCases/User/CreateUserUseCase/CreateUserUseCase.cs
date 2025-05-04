using gamehub_API.Application.DTO.User;
using gamehub_API.Application.Interfaces;
using gamehub_API.Infrastructure.Models;

namespace gamehub_API.Application.UseCases.User.CreateUserUseCase
{
    public class CreateUserUseCase : ICreateUserUseCase
    {
        private readonly IUserInterface _userInterface;

        public CreateUserUseCase(IUserInterface userInterface)
        {
            _userInterface = userInterface;
        }

        public async Task ExecuteAsync(CreateUserDTO createUserDTO)
        {
            var user = new Users
            {
                id = Guid.NewGuid().ToString(),
                userId = Guid.NewGuid().ToString(),
                username = createUserDTO.username,
                email = createUserDTO.email,
                password = createUserDTO.password,
                firstName = createUserDTO.firstName,
                lastName = createUserDTO.lastName,
                //dateOfBirth = createUserDTO.dateOfBirth,
                role = createUserDTO.role,
            };

            await _userInterface.AddUserAsync(user);
        }
    }
}
