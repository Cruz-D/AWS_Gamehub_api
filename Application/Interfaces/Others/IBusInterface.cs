namespace gamehub_API.Application.Interfaces.Others
{
    public interface IBusInterface
    {
        Task SendMessageAsync(string queueName, string message);
    }
}
