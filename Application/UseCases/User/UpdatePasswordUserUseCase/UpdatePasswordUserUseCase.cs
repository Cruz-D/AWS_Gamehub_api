
using gamehub_API.Application.Interfaces;

namespace gamehub_API.Application.UseCases.User.UpdatePasswordUseCase
{
    public class UpdatePasswordUserUseCase : IUpdatePasswordUserUseCase
    {
        private readonly IUserInterface _userInterface;
        private readonly IPasswordHasher _passwordHasher;

        public UpdatePasswordUserUseCase(IUserInterface userInterface, IPasswordHasher passwordHasher)
        {
            _userInterface = userInterface;
            _passwordHasher = passwordHasher;
        }

        public Task<bool> ExecuteAsync(ChangePasswordDTO changePasswordDTO)
        {
            // Validar los datos de entrada
            if (string.IsNullOrEmpty(changePasswordDTO.userId))
            {
                throw new ArgumentException("El userId no puede ser nulo o vacío.", nameof(changePasswordDTO.userId));
            }

            // Obtener el usuario existente desde CosmosDB
            var existingUser = _userInterface.GetUserByIdAsync(changePasswordDTO.userId).Result;

            // Verificar la contraseña actual
            var isPasswordValid = _passwordHasher.VerifyPassword(changePasswordDTO.oldPassword, existingUser.systemInfo.password);

            if (!isPasswordValid)
            {
                throw new Exception("La contraseña actual no coincide con la de la base de datos.");
            }

            // Generar un nuevo hash para la nueva contraseña
            var newHashedPassword = _passwordHasher.HashPassword(changePasswordDTO.newPassword);

            // Actualizar el hash en el documento del usuario
            existingUser.systemInfo.password = newHashedPassword;

            // Guardar el usuario actualizado en CosmosDB
            var updatedUser = _userInterface.UpdateUserAsync(existingUser).Result;

            if (updatedUser != null)
            {
                return Task.FromResult(true);
            }
            else
            {
                throw new Exception("Error al actualizar la contraseña del usuario en CosmosDB.");
            }
        }

    }
}
