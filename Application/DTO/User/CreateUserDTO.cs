using System.Text.Json.Serialization;

namespace gamehub_API.Application.DTO.User
{
    public struct CreateUserDTO
    {
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
        public string dateOfBirth { get; set; } // Fecha de nacimiento del usuario.

        [JsonPropertyName("profilePictureUrl")]
        public string? profilePictureUrl { get; set; } // URL de la imagen de perfil del usuario.

        [JsonPropertyName("createdAt")]
        public string createdAt { get; set; } // Fecha y hora de creación de la cuenta.

        [JsonPropertyName("country")]
        public string? country { get; set; } // País del usuario.

        [JsonPropertyName("city")]
        public string? city { get; set; } // Ciudad del usuario.
    }
}
