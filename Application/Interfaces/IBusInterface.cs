
namespace gamehub_API.Application.Interfaces
{
    public interface IBusInterface
    {
        Task SendMessageAsync(string queueName, string message);
    }
}
