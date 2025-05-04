using gamehub_API.Application.Interfaces;
using gamehub_API.Infrastructure.Models;
using gamehub_API.Infrastructure.Services.ServiceBus;
using Microsoft.Azure.Cosmos;
using System.Text.Json;

namespace gamehub_API.Infrastructure.Repositories
{
    public class UserRepository : IUserInterface
    {
        private readonly Container _container;
        private readonly IBusInterface? _busServices;
        private readonly IPasswordHasher _passwordHasher;

        public UserRepository(CosmosClient cosmosClient, string databaseName, string containerName, BusServices busServices, IPasswordHasher passwordHasher)
        {
            _container = cosmosClient.GetContainer(databaseName, containerName);
            _busServices = busServices;
            _passwordHasher = passwordHasher;
        }

        public async Task<Users> AddUserAsync(Users user)
        {
            try
            {
                var request = await _container.CreateItemAsync(user, new PartitionKey(user.userId));

                if (request.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    await _busServices!.SendMessageAsync("register", $"Se ha creado un nuevo usuario con ID: {user.id}");

                    return user;
                }
                throw new Exception("Error al crear el usuario en la base de datos.");
            }
            catch (CosmosException ex)
            {
                throw new Exception($"Error en Cosmos DB: {ex.Message}", ex);
            }
        }

        //Refactorizar esto al caso de uso
        public async Task<Users> GetUserByIdAsync(string userId)
        {
            try
            {
                var query = new QueryDefinition("SELECT * FROM c WHERE c.userId = @userId").WithParameter("@userId", userId);
                var iterator = _container.GetItemQueryIterator<Users>(query);
                var response = await iterator.ReadNextAsync();

                return response.FirstOrDefault() ?? throw new Exception($"Usuario con ID {userId} no encontrado.");
            }
            catch (CosmosException ex)
            {
                throw new Exception($"Error en Cosmos DB: {ex.Message}", ex);
            }
        }

        public async Task<Users> UpdateUserAsync(Users user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "El usuario no puede ser nulo.");
            }

            try
            {
                // Guardar los cambios en la base de datos
                var request = await _container.ReplaceItemAsync(user, user.id, new PartitionKey(user.userId));

                if (request.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return request.Resource;
                }

                throw new Exception("Error al actualizar el usuario en la base de datos.");
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new Exception($"No se encontró el usuario con userId {user.userId}.", ex);
            }
            catch (CosmosException ex)
            {
                throw new Exception($"Error en Cosmos DB: {ex.Message}", ex);
            }
        }

        public async Task<Users> DeleteUserAsync(Users user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "El usuario no puede ser nulo.");
            }

            try
            {
                var request = await _container.DeleteItemAsync<Users>(user.id, new PartitionKey(user.userId));
                if (request.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return user;
                }
                throw new Exception("Error al eliminar el usuario de la base de datos.");
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new Exception($"Usuario con ID {user.id} no encontrado.", ex);
            }
            catch (CosmosException ex)
            {
                throw new Exception($"Error en Cosmos DB: {ex.Message}", ex);
            }
        }

        public async Task<Users> UpdatePasswordUserAsync(Users user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "El usuario no puede ser nulo.");
            }

            try
            {
                var request = await _container.ReplaceItemAsync(user, user.id, new PartitionKey(user.userId));

                if (request.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return request.Resource;
                }

                throw new Exception("Error al actualizar la contraseña del usuario en la base de datos.");
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new Exception($"No se encontró el usuario con userId {user.userId}.", ex);
            }
            catch (CosmosException ex)
            {
                throw new Exception($"Error en Cosmos DB: {ex.Message}", ex);
            }
        }

        public async Task<Users> LoginUserAsync(string username, string password)
        {
            

            try
            {
                // Buscar al usuario por nombre de usuario o correo electrónico
                var query = new QueryDefinition(
                    "SELECT * FROM c WHERE c.systemInfo.username = @value OR c.systemInfo.email = @value")
                    .WithParameter("@value", username);

                var iterator = _container.GetItemQueryIterator<Users>(query);
                var response = await iterator.ReadNextAsync();

                var foundUser = response.FirstOrDefault();
                if (foundUser == null)
                {
                    throw new Exception("Usuario no encontrado.");
                }

                return foundUser;
            }
            catch (CosmosException ex)
            {
                throw new Exception($"Error en Cosmos DB: {ex.Message}", ex);
            }
        }

    }
}
