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
            
            ValidateCreateUserDTO(createUserDTO);

            var user = new Users
            {
                userId = _idGenerator.GenerateId(),
                systemInfo = new SystemInfo
                {
                    username = createUserDTO.username,
                    password = _passwordHasher.HashPassword(createUserDTO.password),
                    email = createUserDTO.email,
                    role = "user"
                },
                personalInfo = new PersonalInfo
                {
                    firstName = createUserDTO.firstName,
                    lastName = createUserDTO.lastName,
                    dateOfBirth = createUserDTO.dateOfBirth,
                    profilePictureUrl = createUserDTO.profilePictureUrl
                },
                verification = new Verification
                {
                    isVerified = false,
                    verifiedDate = "verifiedDate"
                },
                authentication = new Authentication
                {
                    isAuthenticated = false,
                    isLoggedIn = false,
                    isBanned = false,
                    refreshToken = "refreshToken",
                    accessToken = "accessToken",
                    tokenExpiry = "tokenExpiry",
                    tokenCreatedAt = "tokenCreatedAt"
                },
                location = new Location
                {
                    country = createUserDTO.country,
                    city = createUserDTO.city
                },
                timestamps = new Timestamps
                {
                    createdAt = DateTime.UtcNow.ToString("o"),
                    updatedAt = "updatedAt",
                    lastLogin = "lastLogin"
                }
            };

            try
            {
                var savedUser = await _userInterface.AddUserAsync(user);

                return new GetUserDTO
                {
                    userId = savedUser.userId,
                    username = user.systemInfo.username,
                    email = user.systemInfo.email,
                    firstName = user.personalInfo.firstName,
                    lastName = user.personalInfo.lastName,
                    profilePictureUrl = user.personalInfo.profilePictureUrl,
                    isVerified = user.verification.isVerified,
                    dateOfBirth = user.personalInfo.dateOfBirth,
                    role = user.systemInfo.role,
                    createdAt = user.timestamps.createdAt,
                    lastLogin = user.timestamps.updatedAt,
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el usuario.", ex);
            }
        }


        // Método para validar los datos de entrada
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

        // Método para validar el formato del correo electrónico
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
