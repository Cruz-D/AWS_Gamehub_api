using System.Text.Json.Serialization;

public struct GetUserDTO
{
    [JsonPropertyName("id")]
    public string id { get; set; }

    [JsonPropertyName("userId")]
    public string userId { get; set; } // Nueva Partition Key.

    [JsonPropertyName("username")]
    public string username { get; set; }

    [JsonPropertyName("email")]
    public string email { get; set; }

    [JsonPropertyName("firstName")]
    public string firstName { get; set; }

    [JsonPropertyName("lastName")]
    public string lastName { get; set; }

    [JsonPropertyName("profilePictureUrl")]
    public string? profilePictureUrl { get; set; }

    [JsonPropertyName("isVerified")]
    public bool? isVerified { get; set; }


    [JsonPropertyName("dateOfBirth")]
    public string? dateOfBirth { get; set; }

    [JsonPropertyName("role")]
    public string role { get; set; }

    [JsonPropertyName("createdAt")]
    public string? createdAt { get; set; }

    [JsonPropertyName("lastLogin")]
    public string? lastLogin { get; set; }
}
