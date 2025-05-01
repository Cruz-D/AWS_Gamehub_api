using System.Threading.Tasks;

namespace gamehub_API.Infrastructure.Services.ServiceBus
{
    public interface IBusServices
    {
        Task SendMessageAsync(string queueName, string message);
    }
}
