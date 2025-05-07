namespace gamehub_API.Application.UseCases.Auth.LoginUserUseCase
{
    public interface ILoginUserUseCase
    {
        Task<LoginResponseUserDTO> ExecuteAsync(LoginUserDTO loginUserDTO);
    }
}
