using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public struct UpdateUserDTO
{
    [Key]
    [JsonIgnore]
    [JsonPropertyName("id")]
    public string? id { get; set; } // ID único del usuario.

    [JsonPropertyName("userId")]
    [Required(ErrorMessage = "El ID del usuario es obligatorio.")]
    [StringLength(36, MinimumLength = 36, ErrorMessage = "El ID del usuario debe tener 36 caracteres (formato UUID).")]
    public string? userId { get; set; } // Nueva Partition Key.

    [JsonPropertyName("email")]
    [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
    [StringLength(100, ErrorMessage = "El correo electrónico no puede exceder los 100 caracteres.")]
    public string? email { get; set; } // Dirección de correo electrónico.

    [JsonPropertyName("firstName")]
    [StringLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres.")]
    public string? firstName { get; set; } // Nombre del usuario.

    [JsonPropertyName("lastName")]
    [StringLength(50, ErrorMessage = "El apellido no puede exceder los 50 caracteres.")]
    public string? lastName { get; set; } // Apellido del usuario.

    [JsonPropertyName("dateOfBirth")]
    [DataType(DataType.Date, ErrorMessage = "El formato de la fecha de nacimiento no es válido.")]
    public string? dateOfBirth { get; set; } // Fecha de nacimiento.

    [JsonPropertyName("country")]
    [StringLength(50, ErrorMessage = "El país no puede exceder los 50 caracteres.")]
    public string? country { get; set; } // País del usuario.

    [JsonPropertyName("city")]
    [StringLength(50, ErrorMessage = "La ciudad no puede exceder los 50 caracteres.")]
    public string? city { get; set; } // Ciudad del usuario.

    [JsonPropertyName("profilePictureUrl")]
    [Url(ErrorMessage = "El formato de la URL de la imagen de perfil no es válido.")]
    public string? profilePictureUrl { get; set; } // URL de la imagen de perfil.

    [JsonPropertyName("isVerified")]
    public bool? isVerified { get; set; } // Estado de verificación del usuario.

    [JsonPropertyName("isBanned")]
    public bool? isBanned { get; set; } // Estado de baneo del usuario.

    [JsonPropertyName("updatedAt")]
    [DataType(DataType.DateTime, ErrorMessage = "El formato de la fecha de actualización no es válido.")]
    public string? updatedAt { get; set; } // Fecha de última actualización.
}
