namespace gamehub_API.Application.Interfaces.Others
{
    public interface IAuthInterface
    {
        Task<Users> GetUserByUsernameOrEmailAsync(string username);

    }
}
