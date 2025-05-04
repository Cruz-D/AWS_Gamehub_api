using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public struct UpdateUserDTO
{
    [Key]

    [JsonIgnore]
    [JsonPropertyName("id")]
    public string? id { get; set; }

    [JsonPropertyName("userId")]
    public string? userId { get; set; } // Nueva Partition Key.

    [JsonPropertyName("email")]
    public string? email { get; set; }

    [JsonPropertyName("firstName")]
    public string? firstName { get; set; }

    [JsonPropertyName("lastName")]
    public string? lastName { get; set; }

    [JsonPropertyName("dateOfBirth")]
    public string? dateOfBirth { get; set; }

    //[JsonPropertyName("role")]
    //public string role { get; set; }
}
