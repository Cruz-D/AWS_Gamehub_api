using Azure.Messaging.ServiceBus;

namespace gamehub_API.Infrastructure.Services.ServiceBus
{
    public class BusServices : IBusServices
    {
        // Inyección de dependencias para el cliente de Service Bus
        private readonly ServiceBusClient _serviceBusClient;

        // Constructor que recibe el cliente de Service Bus
        public BusServices(ServiceBusClient serviceBusClient)
        {
            // Asignar el cliente de Service Bus a la variable de instancia
            _serviceBusClient = serviceBusClient;
        }

        public async Task SendMessageAsync(string queueName, string message)
        {
            // Crear un sender para enviar mensajes a la cola especificada
            ServiceBusSender sender = _serviceBusClient.CreateSender(queueName);

            // Crear un mensaje de Service Bus con el contenido del mensaje
            ServiceBusMessage serviceBusMessage = new ServiceBusMessage(message);

            // Enviar el mensaje a la cola
            await sender.SendMessageAsync(serviceBusMessage);
        }
    }
}
