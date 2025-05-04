using System.Text.Json.Serialization;

namespace gamehub_API.Application.DTO.User
{
    public struct CreateUserDTO
    {
        [JsonPropertyName("id")]
        public string? id { get; set; } // Identificador único del usuario.

        [JsonPropertyName("userId")]
        public string? userId { get; set; } // Nueva Partition Key.

        [JsonPropertyName("username")]
        public string username { get; set; } // Nombre de usuario único.

        [JsonPropertyName("password")]
        public string password { get; set; } // Contraseña del usuario (debe almacenarse como hash).

        [JsonPropertyName("email")]
        public string email { get; set; } // Dirección de correo electrónico del usuario.

        [JsonPropertyName("role")]
        public string role { get; set; } // Rol del usuario (e.g., Admin, User, Moderator).

        [JsonPropertyName("firstName")]
        public string firstName { get; set; } // Nombre del usuario.

        [JsonPropertyName("lastName")]
        public string lastName { get; set; } // Apellido del usuario.

        [JsonPropertyName("dateOfBirth")]
        public string? dateOfBirth { get; set; } // Fecha de nacimiento del usuario.
    }
}
