using System.Text.Json.Serialization;

public struct ChangePasswordDTO
{
    [JsonPropertyName("userId")]
    public string userId { get; set; }

    [JsonPropertyName("oldPassword")]
    public string oldPassword { get; set; }

    [JsonPropertyName("newPassword")]
    public string newPassword { get; set; }
}
