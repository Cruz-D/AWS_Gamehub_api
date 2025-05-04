using gamehub_API.Application.DTO.User;
using gamehub_API.Application.Interfaces;
using gamehub_API.Infrastructure.Models;

namespace gamehub_API.Application.UseCases.User.CreateUserUseCase
{
    public class CreateUserUseCase : ICreateUserUseCase
    {
        private readonly IUserInterface _userInterface;
        private readonly IPasswordHasher _passwordHasher; 
        private readonly IIdGenerator _idGenerator; 

        public CreateUserUseCase(IUserInterface userInterface, IPasswordHasher passwordHasher, IIdGenerator idGenerator)
        {
            _userInterface = userInterface;
            _passwordHasher = passwordHasher;
            _idGenerator = idGenerator;
        }

        public async Task<GetUserDTO> ExecuteAsync(CreateUserDTO createUserDTO)
        {
            // Validar los datos de entrada
            ValidateCreateUserDTO(createUserDTO);

            // Crear el modelo de usuario
            var user = new Users
            {
                id = _idGenerator.GenerateId(), // Generar un ID único
                userId = _idGenerator.GenerateId(), // Generar un userId único
                username = createUserDTO.username,
                email = createUserDTO.email,
                password = _passwordHasher.HashPassword(createUserDTO.password), // Hashear la contraseña
                firstName = createUserDTO.firstName,
                lastName = createUserDTO.lastName,
                dateOfBirth = createUserDTO.dateOfBirth,
                role = createUserDTO.role,
            };

            try
            {
                // Guardar el usuario en el repositorio
                await _userInterface.AddUserAsync(user);

                // Mapear el modelo a DTO de salida
                var userDto = new GetUserDTO
                {
                    id = user.id,
                    userId = user.userId,
                    username = user.username,
                    email = user.email,
                    firstName = user.firstName,
                    lastName = user.lastName,
                    dateOfBirth = user.dateOfBirth,
                    role = user.role
                };

                return userDto;

            }
            catch (Exception ex)
            {
                // Manejar excepciones específicas si es necesario
                throw new Exception("Error al crear el usuario.", ex);
            }
        }

        //metodo para validar los datos de entrada
        private void ValidateCreateUserDTO(CreateUserDTO createUserDTO)
        {
            switch (createUserDTO)
            {
                case { username: null or "" }:
                    throw new ArgumentException("El nombre de usuario no puede ser nulo o vacío.", nameof(createUserDTO.username));

                case { email: null or "" }:
                    throw new ArgumentException("El correo electrónico no puede ser nulo o vacío.", nameof(createUserDTO.email));

                case { password: null or "" }:
                    throw new ArgumentException("La contraseña no puede ser nula o vacía.", nameof(createUserDTO.password));

                default:
                    if (!IsValidEmail(createUserDTO.email))
                        throw new ArgumentException("El formato del correo electrónico no es válido.", nameof(createUserDTO.email));
                    break;
            }
        }

        //metodo para validar el formato del correo electronico
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
