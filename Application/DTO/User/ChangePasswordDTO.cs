using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public struct ChangePasswordDTO
{
    [JsonPropertyName("userId")]
    [Required(ErrorMessage = "El ID del usuario es obligatorio.")]
    [StringLength(36, MinimumLength = 36, ErrorMessage = "El ID del usuario debe tener 36 caracteres (formato UUID).")]
    public string userId { get; set; }

    [JsonPropertyName("oldPassword")]
    [Required(ErrorMessage = "La contraseña actual es obligatoria.")]
    [StringLength(100, MinimumLength = 8, ErrorMessage = "La contraseña actual debe tener al menos 8 caracteres.")]
    public string oldPassword { get; set; }

    [JsonPropertyName("newPassword")]
    [Required(ErrorMessage = "La nueva contraseña es obligatoria.")]
    [StringLength(100, MinimumLength = 8, ErrorMessage = "La nueva contraseña debe tener al menos 8 caracteres.")]
    public string newPassword { get; set; }
}
