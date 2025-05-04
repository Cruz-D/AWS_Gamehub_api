namespace gamehub_API.Application.UseCases.User.UpdatePasswordUseCase
{
    public interface IUpdatePasswordUserUseCase
    {
        Task<bool> ExecuteAsync(ChangePasswordDTO changePasswordDTO);
    }
}
