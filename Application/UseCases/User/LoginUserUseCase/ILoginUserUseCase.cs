namespace gamehub_API.Application.UseCases.User.LoginUserUseCase
{
    public interface ILoginUserUseCase
    {
        Task<LoginResponseUserDTO> ExecuteAsync(LoginUserDTO loginUserDTO);
    }
}
