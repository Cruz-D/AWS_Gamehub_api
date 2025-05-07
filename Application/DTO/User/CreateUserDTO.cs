using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace gamehub_API.Application.DTO.User
{
    public struct CreateUserDTO
    {
        [JsonPropertyName("username")]
        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "El nombre de usuario debe tener entre 3 y 20 caracteres.")]
        public string username { get; set; } // Nombre de usuario único.

        [JsonPropertyName("password")]
        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
        public string password { get; set; } // Contraseña del usuario (debe almacenarse como hash).

        [JsonPropertyName("email")]
        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        public string email { get; set; } // Dirección de correo electrónico del usuario.

        [JsonPropertyName("role")]
        [Required(ErrorMessage = "El rol es obligatorio.")]
        [RegularExpression("^(Admin|User|Moderator)$", ErrorMessage = "El rol debe ser Admin, User o Moderator.")]
        public string role { get; set; } // Rol del usuario.

        [JsonPropertyName("firstName")]
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres.")]
        public string firstName { get; set; } // Nombre del usuario.

        [JsonPropertyName("lastName")]
        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [StringLength(50, ErrorMessage = "El apellido no puede exceder los 50 caracteres.")]
        public string lastName { get; set; } // Apellido del usuario.

        [JsonPropertyName("dateOfBirth")]
        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
        [DataType(DataType.Date, ErrorMessage = "El formato de la fecha de nacimiento no es válido.")]
        public string dateOfBirth { get; set; } // Fecha de nacimiento del usuario.

        [JsonPropertyName("profilePictureUrl")]
        [Url(ErrorMessage = "El formato de la URL de la imagen de perfil no es válido.")]
        public string? profilePictureUrl { get; set; } // URL de la imagen de perfil del usuario.

        [JsonPropertyName("createdAt")]
        [Required(ErrorMessage = "La fecha de creación es obligatoria.")]
        [DataType(DataType.DateTime, ErrorMessage = "El formato de la fecha de creación no es válido.")]
        public string createdAt { get; set; } // Fecha y hora de creación de la cuenta.

        [JsonPropertyName("country")]
        [StringLength(50, ErrorMessage = "El país no puede exceder los 50 caracteres.")]
        public string? country { get; set; } // País del usuario.

        [JsonPropertyName("city")]
        [StringLength(50, ErrorMessage = "La ciudad no puede exceder los 50 caracteres.")]
        public string? city { get; set; } // Ciudad del usuario.
    }
}
