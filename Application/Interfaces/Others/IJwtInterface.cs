namespace gamehub_API.Application.Interfaces.Others
{
    public interface IJwtInterface
    {
        string GenerateToken(string userId, string username, string role, string? tokenType);

        string GenerateRefreshToken();


    }
}
