using System.Text.Json.Serialization;

public struct LoginResponseUserDTO
{
    [JsonPropertyName("userId")]
    public string? userId { get; set; }

    [JsonPropertyName("isAuthenticated")]
    public bool IsAuthenticated { get; set; }

    [JsonPropertyName("accessToken")]
    public string? accessToken { get; set; }

    [JsonPropertyName("refreshToken")]
    public string? refreshToken { get; set; }

    [JsonPropertyName("tokenExpiry")]
    public string? tokenExpiry { get; set; }

    [JsonPropertyName("tokenCreatedAt")]
    public string? tokenCreatedAt { get; set; }

    [JsonPropertyName("lastLogin")]
    public string? LastLogin { get; set; }
}
