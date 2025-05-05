namespace gamehub_API.Application.Interfaces
{
    public interface IJwtInterface
    {
        string GenerateToken(string userId, string username, string role, string? tokenType);

        string GenerateRefreshToken();


    }
}
