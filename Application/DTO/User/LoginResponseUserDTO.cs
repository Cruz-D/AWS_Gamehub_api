using System.Text.Json.Serialization;

public struct LoginResponseUserDTO
{
    [JsonPropertyName("userId")]
    public string UserId { get; set; }

    [JsonPropertyName("username")]
    public string Username { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("isAuthenticated")]
    public bool IsAuthenticated { get; set; }

    [JsonPropertyName("lastLogin")]
    public string? LastLogin { get; set; }
}
