namespace gamehub_API.Application.Interfaces.Others
{
    public interface IJwtInterface
    {
        string GenerateToken(string userId, string username, string email);

        string GenerateRefreshToken();


    }
}
