using gamehub_API.Application.Interfaces;
using gamehub_API.Application.Interfaces.Others;
using gamehub_API.Infrastructure.Models;
using gamehub_API.Infrastructure.Services.ServiceBus;
using Microsoft.Azure.Cosmos;

namespace gamehub_API.Infrastructure.Repositories
{
    public class VideogameRepository : IVideogameInterface
    {
        //Instanciar el contenedor de Cosmos DB
        private readonly Container _container;

        private readonly IBusInterface? _busServices;

        //Constructor que recibe el cliente de Cosmos DB, el nombre de la base de datos y el nombre del contenedor
        public VideogameRepository(
            CosmosClient cosmosClient, 
            string databaseName, 
            string containerName,
            BusServices busServices)
        {
            _container = cosmosClient.GetContainer(databaseName, containerName);
            _busServices = busServices;
        }

        public async Task<List<Videogame>> GetAllVideogamesAsync(string sqlCosmosQuery)
        {
            try
            {
                //Ejecutar la consulta SQL en Cosmos DB
                var result = await _container.GetItemQueryIterator<Videogame>(new QueryDefinition(sqlCosmosQuery)).ReadNextAsync();

                //Devolver la lista de videojuegos
                return result.ToList();

            }
            catch (CosmosException ex)
            {
                // Manejar errores específicos de Cosmos DB
                throw new Exception($"\n Error en Cosmos DB: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejar otros errores
                throw new Exception($"\n Error inesperado: {ex.Message}", ex);
            }

        }

        public async Task<Videogame> GetVideogameByIdAsync(string id)
        {
            try
            {
                //Ejecutar la consulta de cosmos
                var result = await _container.GetItemQueryIterator<Videogame>(new QueryDefinition("SELECT * FROM c WHERE c.id = @id")
                    .WithParameter("@id", id)).ReadNextAsync();

                await _busServices!.SendMessageAsync("videogames-queue", $"Se ha solicitado el videojuego con id: {id}");


                //Devolver el videojuego encontrado
                if (result.Count == 0)
                {
                    throw new Exception($"\n No se encontró el videojuego con id: {id}");
                }
                return result.FirstOrDefault()!;
            }
            catch (CosmosException ex)
            {
                // Manejar errores específicos de Cosmos DB
                throw new Exception($"\n Error en Cosmos DB: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejar otros errores
                throw new Exception($"\n Error inesperado: {ex.Message}", ex);
            }
        }

    }

}
