using System.Text.Json.Serialization;

public struct AuthenticateUserDTO
{
    [JsonPropertyName("usernameOrEmail")]
    public string usernameOrEmail { get; set; }

    [JsonPropertyName("password")]
    public string password { get; set; }
}
