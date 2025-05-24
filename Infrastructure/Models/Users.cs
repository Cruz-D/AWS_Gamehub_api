using Amazon.DynamoDBv2.DataModel;

// Estructura de la tabla de DynamoDB
[DynamoDBTable("Users")] // Correct attribute for specifying the table name
public class Users
{
    //==================================================

    [DynamoDBHashKey] // Clave primaria (Partition Key)
    [DynamoDBProperty("userId")]
    public string? userId { get; set; }

    //==================================================

    [DynamoDBProperty("systemInfo")]
    public SystemInfo? systemInfo { get; set; }

    [DynamoDBProperty("personalInfo")]
    public PersonalInfo? personalInfo { get; set; }

    [DynamoDBProperty("verification")]
    public Verification? verification { get; set; }

    [DynamoDBProperty("authentication")]
    public Authentication? authentication { get; set; }

    [DynamoDBProperty("location")]
    public Location? location { get; set; }

    [DynamoDBProperty("timestamps")]
    public Timestamps? timestamps { get; set; }

    //==================================================
}

public class SystemInfo
{
    [DynamoDBProperty("username")]
    public string? username { get; set; }

    [DynamoDBProperty("password")]
    public string? password { get; set; }

    [DynamoDBProperty("email")]
    public string? email { get; set; }

    [DynamoDBProperty("role")]
    public string? role { get; set; }
}

public class PersonalInfo
{
    [DynamoDBProperty("firstName")]
    public string? firstName { get; set; }

    [DynamoDBProperty("lastName")]
    public string? lastName { get; set; }

    [DynamoDBProperty("dateOfBirth")]
    public string? dateOfBirth { get; set; }

    [DynamoDBProperty("profilePictureUrl")]
    public string? profilePictureUrl { get; set; }
}

public class Verification
{
    [DynamoDBProperty("isVerified")]
    public bool? isVerified { get; set; }

    [DynamoDBProperty("verifiedDate")]
    public string? verifiedDate { get; set; }
}

public class Authentication
{
    [DynamoDBProperty("isAuthenticated")]
    public bool? isAuthenticated { get; set; }

    [DynamoDBProperty("isLoggedIn")]
    public bool? isLoggedIn { get; set; }

    [DynamoDBProperty("isBanned")]
    public bool? isBanned { get; set; }

    [DynamoDBProperty("refreshToken")]
    public string? refreshToken { get; set; }

    [DynamoDBProperty("accessToken")]
    public string? accessToken { get; set; }

    [DynamoDBProperty("tokenExpiry")]
    public string? tokenExpiry { get; set; }

    [DynamoDBProperty("tokenCreatedAt")]
    public string? tokenCreatedAt { get; set; }
}

public class Location
{
    [DynamoDBProperty("country")]
    public string? country { get; set; }

    [DynamoDBProperty("city")]
    public string? city { get; set; }
}

public class Timestamps
{
    [DynamoDBProperty("createdAt")]
    public string? createdAt { get; set; }

    [DynamoDBProperty("updatedAt")]
    public string? updatedAt { get; set; }

    [DynamoDBProperty("lastLogin")]
    public string? lastLogin { get; set; }
}
