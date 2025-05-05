using Newtonsoft.Json;

namespace gamehub_API.Application.DTO.User
{
    public class LogOutUserDTO
    {
        
        [JsonProperty("userId")]
        public string? userId { get; set; } // ID del usuario que desea cerrar sesión

        [JsonProperty("token")]
        public string? token { get; set; }  // Token JWT del usuario
    }
}