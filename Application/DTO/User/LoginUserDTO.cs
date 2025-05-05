using System.Text.Json.Serialization;

public struct LoginUserDTO
{
    [JsonPropertyName("usernameOrEmail")]
    public string usernameOrEmail { get; set; }

    [JsonPropertyName("password")]
    public string password { get; set; }
}
