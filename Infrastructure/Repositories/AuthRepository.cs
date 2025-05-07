using gamehub_API.Application.Interfaces.Others;
using Microsoft.Azure.Cosmos;

namespace gamehub_API.Infrastructure.Repositories
{
    public class AuthRepository : IAuthInterface
    {
        private readonly Container _container;

        public AuthRepository(CosmosClient cosmosClient, string databaseName, string containerName)
        {
            _container = cosmosClient.GetContainer(databaseName, containerName);
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
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new Exception($"No se encontró el usuario identificado {username}.", ex);
            }
            catch (CosmosException ex)
            {
                throw new Exception($"Error en Cosmos DB: {ex.Message}", ex);
            }
        }
    }

}
