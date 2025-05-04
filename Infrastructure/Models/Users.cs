using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace gamehub_API.Infrastructure.Models
{
    public class Users
    {
        // ============================================
        // Identificación del usuario
        // ============================================
        [Key]
        [JsonPropertyName("id")]
        public string id { get; set; } // Identificador único del usuario.

        [JsonPropertyName("userId")]
        public string userId { get; set; } // Nueva Partition Key.

        [JsonPropertyName("username")]
        public string username { get; set; } // Nombre de usuario único.

        [JsonPropertyName("password")]
        public string password { get; set; } // Contraseña del usuario (debe almacenarse como hash).

        [JsonPropertyName("email")]
        public string email { get; set; } // Dirección de correo electrónico del usuario.

        // ============================================
        // Verificación y roles
        // ============================================
        //[JsonPropertyName("isVerified")]
        //public bool? IsVerified { get; set; } // Indica si el usuario ha verificado su cuenta.

        [JsonPropertyName("role")]
        public string role { get; set; } // Rol del usuario (e.g., Admin, User, Moderator).

        // ============================================
        // Información personal
        // ============================================
        [JsonPropertyName("firstName")]
        public string firstName { get; set; } // Nombre del usuario.

        [JsonPropertyName("lastName")]
        public string lastName { get; set; } // Apellido del usuario.

        [JsonPropertyName("dateOfBirth")]
        public string? dateOfBirth { get; set; } // Fecha de nacimiento del usuario.

        //[JsonPropertyName("profilePictureUrl")]
        //public string ProfilePictureUrl { get; set; } // URL de la imagen de perfil del usuario.

        // ============================================
        // Fechas importantes
        // ============================================
        //[JsonPropertyName("createdAt")]
        //public DateTime CreatedAt { get; set; } // Fecha y hora de creación de la cuenta.

        //[JsonPropertyName("updatedAt")]
        //public DateTime UpdatedAt { get; set; } // Fecha y hora de la última actualización de la cuenta.

        //[JsonPropertyName("lastLogin")]
        //public DateTime? LastLogin { get; set; } // Fecha y hora del último inicio de sesión.

        // ============================================
        // Personalización
        //// ============================================
        //[JsonPropertyName("preferences")]
        //public string Preferences { get; set; } // Configuraciones personalizadas del usuario en formato JSON.

        // ============================================
        // Ubicación
        // ============================================
        //[JsonPropertyName("country")]
        //public string Country { get; set; } // País del usuario.

        //[JsonPropertyName("city")]
        //public string City { get; set; } // Ciudad del usuario.

        //[JsonPropertyName("timeZone")]
        //public string TimeZone { get; set; } // Zona horaria del usuario.

        // ============================================
        // Analítica
        // ============================================
        //[JsonPropertyName("deviceType")]
        //public string DeviceType { get; set; } // Tipo de dispositivo usado (e.g., móvil, escritorio).

        //[JsonPropertyName("operatingSystem")]
        //public string OperatingSystem { get; set; } // Sistema operativo del dispositivo.

        //[JsonPropertyName("browser")]
        //public string Browser { get; set; } // Navegador usado para acceder a la app.

        //[JsonPropertyName("ipAddress")]
        //public string IpAddress { get; set; } // Dirección IP del usuario.

        // ============================================
        // Monetización
        //// ============================================
        //[JsonPropertyName("subscriptionPlan")]
        //public string SubscriptionPlan { get; set; } // Plan de suscripción del usuario (e.g., Free, Premium).

        //[JsonPropertyName("subscriptionExpiry")]
        //public DateTime? SubscriptionExpiry { get; set; } // Fecha de expiración de la suscripción.
    }
}
