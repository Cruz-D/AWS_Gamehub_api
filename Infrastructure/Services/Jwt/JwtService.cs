using System.IdentityModel.Tokens.Jwt; // Biblioteca para manejar tokens JWT.
using System.Security.Claims; // Proporciona clases para trabajar con claims (información del usuario en el token).
using Microsoft.IdentityModel.Tokens; // Proporciona clases para manejar la seguridad de los tokens.
using System.Text; // Para trabajar con codificación de texto.
using gamehub_API.Application.Interfaces;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using gamehub_API.Application.Interfaces.Others; // Interfaz que implementa esta clase.

public class JwtService : IJwtInterface
{
    private readonly IConfiguration _configuration;

    // Constructor que inyecta la configuración de la aplicación (appsettings.json).
    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    // Método para generar un token de acceso (JWT).
    public string GenerateToken(string userId, string username, string email, string tokenType)
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
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds // Credenciales de firma para garantizar la integridad del token.
        );


        // validar el token y devolverlo.

        var validate = ValidateToken(new JwtSecurityTokenHandler().WriteToken(token));

        if (validate == null)
        {
            throw new SecurityTokenException("Token inválido");
        }

        // Si la validación es exitosa, se devuelve el token.
        return new JwtSecurityTokenHandler().WriteToken(token);
    }


    // Método para validar el token JWT.
    public ClaimsPrincipal ValidateToken(string token)
    {
        // Obtener la clave secreta desde la configuración (appsettings.json).
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

        // Configurar los parámetros de validación del token.
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true, // Validar la clave de firma.
            IssuerSigningKey = key, // Clave de firma.
            ValidateIssuer = true, // Validar el emisor.
            ValidIssuer = _configuration["Jwt:Issuer"], // Emisor esperado.
            ValidateAudience = true, // Validar la audiencia.
            ValidAudience = _configuration["Jwt:Audience"], // Audiencia esperada.
            ValidateLifetime = true, // Validar la fecha de expiración del token.
            ClockSkew = TimeSpan.Zero // No permitir margen de error en la fecha de expiración.
        };

        // Crear un manejador de tokens JWT para validar el token.
        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            // Validar el token y devolver los claims (información del usuario).
            return tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);
        }
        catch (Exception ex)
        {
            throw new SecurityTokenException("Token inválido", ex);
        }
    }

    // Método para generar un refresh token (token de actualización).
    public string GenerateRefreshToken()
    {
        // Crear un array de bytes para almacenar un número aleatorio.
        var randomNumber = new byte[32];

        // Usar RandomNumberGenerator para llenar el array con valores aleatorios.
        RandomNumberGenerator.Fill(randomNumber); // Generar los bytes aleatorios.
        return Convert.ToBase64String(randomNumber); // Convertir los bytes a una cadena en formato Base64.
    }

    // Método para validar el refresh token.
    public async Task<bool> ValidateRefreshTokenAsync(string userId, string refreshToken, IUserInterface userInterface)
    {
        var user = await userInterface.GetUserByIdAsync(userId);
        if (user == null || user.authentication == null)
            return false;

        return user.authentication.refreshToken == refreshToken &&
               DateTime.UtcNow <= DateTime.Parse(user.authentication.tokenExpiry);
    }

    // Método para revocar el refresh token.
    public async Task RevokeRefreshTokenAsync(string userId, IUserInterface userInterface)
    {
        var user = await userInterface.GetUserByIdAsync(userId);
        if (user != null && user.authentication != null)
        {
            user.authentication.refreshToken = null;
            user.authentication.tokenExpiry = null;
            await userInterface.UpdateUserAsync(user);
        }
    }
}
