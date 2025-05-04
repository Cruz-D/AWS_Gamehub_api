using System.Text.Json.Serialization;

public struct DeleteUserDTO
{
    [JsonPropertyName("id")]
    public string id { get; set; }
}
