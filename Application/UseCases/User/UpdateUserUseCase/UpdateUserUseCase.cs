using gamehub_API.Application.Interfaces;
using gamehub_API.Infrastructure.Models;

namespace gamehub_API.Application.UseCases.User.EditUserUseCase
{
    public class UpdateUserUseCase : IUpdateUserUseCase
    {
        private readonly IUserInterface _userInterface;

        public UpdateUserUseCase(IUserInterface userInterface)
        {
            _userInterface = userInterface;
        }

        public async Task<GetUserDTO> ExecuteAsync(UpdateUserDTO updateUserDTO)
        {
            if (string.IsNullOrEmpty(updateUserDTO.userId))
            {
                throw new ArgumentException("El userId no puede ser nulo o vacío.", nameof(updateUserDTO.userId));
            }

            try
            {
                // Obtener el usuario existente desde el repositorio
                var existingUser = await _userInterface.GetUserByIdAsync(updateUserDTO.userId);

                if (existingUser == null)
                {
                    throw new Exception($"Usuario con userId {updateUserDTO.userId} no encontrado.");
                }

                // Actualizar los campos del modelo existente con los valores del DTO
                existingUser.id = string.IsNullOrEmpty(updateUserDTO.id) ? existingUser.id : updateUserDTO.id;
                existingUser.userId = string.IsNullOrEmpty(updateUserDTO.userId) ? existingUser.userId : updateUserDTO.userId;
                existingUser.email = string.IsNullOrEmpty(updateUserDTO.email) ? existingUser.email : updateUserDTO.email;
                existingUser.firstName = string.IsNullOrEmpty(updateUserDTO.firstName) ? existingUser.firstName : updateUserDTO.firstName;
                existingUser.lastName = string.IsNullOrEmpty(updateUserDTO.lastName) ? existingUser.lastName : updateUserDTO.lastName;
                existingUser.dateOfBirth = string.IsNullOrEmpty(updateUserDTO.dateOfBirth) ? existingUser.dateOfBirth : updateUserDTO.dateOfBirth;

                // Actualizar el usuario en el repositorio
                var updatedUser = await _userInterface.UpdateUserAsync(existingUser);

                // Mapear el modelo actualizado al DTO de salida
                var userDto = new GetUserDTO
                {
                    id = updatedUser.id,
                    userId = updatedUser.userId,
                    username = updatedUser.username,
                    email = updatedUser.email,
                    firstName = updatedUser.firstName,
                    lastName = updatedUser.lastName,
                    dateOfBirth = updatedUser.dateOfBirth,
                    role = updatedUser.role
                };

                return userDto;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el usuario.", ex);
            }
        }
    }
}
