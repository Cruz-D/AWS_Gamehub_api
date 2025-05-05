using System.IdentityModel.Tokens.Jwt; // Biblioteca para manejar tokens JWT.
using System.Security.Claims; // Proporciona clases para trabajar con claims (información del usuario en el token).
using Microsoft.IdentityModel.Tokens; // Proporciona clases para manejar la seguridad de los tokens.
using System.Text; // Para trabajar con codificación de texto.
using gamehub_API.Application.Interfaces; // Interfaz que implementa esta clase.

public class JwtService : IJwtInterface
{
    private readonly IConfiguration _configuration;

    // Constructor que inyecta la configuración de la aplicación (appsettings.json).
    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    // Método para generar un token de acceso (JWT).
    public string GenerateToken(string userId, string username, string email)
    {
        // Definir los claims (información del usuario) que se incluirán en el token.
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId), // Identificador único del usuario.
            new Claim(JwtRegisteredClaimNames.UniqueName, username), // Nombre de usuario.
            new Claim(JwtRegisteredClaimNames.Email, email), // Correo electrónico del usuario.
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // Identificador único del token (para evitar reutilización).
        };

        // Obtener la clave secreta desde la configuración (appsettings.json).
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

        // Crear las credenciales de firma utilizando la clave secreta y el algoritmo HMAC-SHA256.
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // Crear el token JWT con los datos configurados.
        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"], // Emisor del token (por ejemplo, tu API).
            audience: _configuration["Jwt:Audience"], // Audiencia del token (por ejemplo, los clientes que consumen tu API).
            claims: claims, // Claims que se incluirán en el token.
            expires: DateTime.UtcNow.AddHours(1), // Fecha de expiración del token (1 hora desde su creación).
            signingCredentials: creds // Credenciales de firma para garantizar la integridad del token.
        );

        // Serializar el token a una cadena (formato JWT) y devolverlo.
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    //TODO: Implementar la validación del token y la verificación de los claims.

}
