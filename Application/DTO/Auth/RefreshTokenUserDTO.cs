using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace gamehub_API.Application.DTO.Auth
{
    public class RefreshTokenDTO
    {
        [JsonPropertyName("userId")]
        [Required(ErrorMessage = "El ID del usuario es obligatorio.")]
        public string? UserId { get; set; }

        [JsonPropertyName("refreshToken")]
        [Required(ErrorMessage = "El refresh token es obligatorio.")]
        public string? RefreshToken { get; set; }
    }
}
