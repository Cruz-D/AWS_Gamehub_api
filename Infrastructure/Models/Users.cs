using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

//Estructura de la tabla de CosmosDb
public class Users
{

    //==================================================

    [Key]
    [JsonPropertyName("id")]
    public string? id { get; set; }

    [JsonPropertyName("userId")]
    public string? userId { get; set; }

    //==================================================

    [JsonPropertyName("SystemInfo")]
    public SystemInfo? systemInfo { get; set; } 

    [JsonPropertyName("personalInfo")]
    public PersonalInfo? personalInfo { get; set; }

    [JsonPropertyName("verification")]
    public Verification? verification { get; set; }

    [JsonPropertyName("authentication")]
    public Authentication? authentication { get; set; }

    [JsonPropertyName("location")]
    public Location? location { get; set; }

    [JsonPropertyName("timestamps")]
    public Timestamps? timestamps { get; set; }

    //==================================================

}

public class SystemInfo
{
    
    [JsonPropertyName("username")]
    public string? username { get; set; }

    [JsonPropertyName("password")]
    public string? password { get; set; }

    [JsonPropertyName("email")]
    public string? email { get; set; }

    [JsonPropertyName("role")]
    public string? role { get; set; }
}


public class PersonalInfo
{
    [JsonPropertyName("firstName")]
    public string? firstName { get; set; }

    [JsonPropertyName("lastName")]
    public string? lastName { get; set; }

    [JsonPropertyName("dateOfBirth")]
    public string? dateOfBirth { get; set; }

    [JsonPropertyName("profilePictureUrl")]
    public string? profilePictureUrl { get; set; }
}

public class Verification
{
    [JsonPropertyName("isVerified")]
    public bool? isVerified { get; set; }

    [JsonPropertyName("verifiedDate")]
    public string? verifiedDate { get; set; }
}

public class Authentication
{
    [JsonPropertyName("isAuthenticated")]
    public bool? isAuthenticated { get; set; }

    [JsonPropertyName("isLoggedIn")]
    public bool? isLoggedIn { get; set; }

    [JsonPropertyName("isBanned")]
    public bool? isBanned { get; set; }

    [JsonPropertyName("refreshToken")]
    public string? refreshToken { get; set; }

    [JsonPropertyName("accessToken")]
    public string? accessToken { get; set; }

    [JsonPropertyName("tokenExpiry")]
    public string? tokenExpiry { get; set; }

    [JsonPropertyName("tokenCreatedAt")]
    public string? tokenCreatedAt { get; set; }
}

public class Location
{
    [JsonPropertyName("country")]
    public string? country { get; set; }

    [JsonPropertyName("city")]
    public string? city { get; set; }
}

public class Timestamps
{
    [JsonPropertyName("createdAt")]
    public string? createdAt { get; set; }

    [JsonPropertyName("updatedAt")]
    public string? updatedAt { get; set; }

    [JsonPropertyName("lastLogin")]
    public string? lastLogin { get; set; }
}


