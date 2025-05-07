namespace gamehub_API.Application.Interfaces.Others
{
    public interface IAuthInterface
    {
        Task<Users> LoginUserAsync(string username, string password);

    }
}
