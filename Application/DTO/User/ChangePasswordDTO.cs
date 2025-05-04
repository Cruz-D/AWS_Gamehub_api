using System.Text.Json.Serialization;

public struct ChangePasswordDTO
{
    [JsonPropertyName("id")]
    public string id { get; set; }

    [JsonPropertyName("oldPassword")]
    public string oldPassword { get; set; }

    [JsonPropertyName("newPassword")]
    public string newPassword { get; set; }
}
