using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public struct LoginUserDTO
{
    [JsonPropertyName("usernameOrEmail")]
    [Required(ErrorMessage = "El nombre de usuario o correo electrónico es obligatorio.")]
    [StringLength(100, ErrorMessage = "El nombre de usuario o correo electrónico no puede exceder los 100 caracteres.")]
    public string usernameOrEmail { get; set; }

    [JsonPropertyName("password")]
    [Required(ErrorMessage = "La contraseña es obligatoria.")]
    [StringLength(100, MinimumLength = 8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
    public string password { get; set; }
}
