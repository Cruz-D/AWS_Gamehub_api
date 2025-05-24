using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2;
using gamehub_API.Application.Interfaces;
using gamehub_API.Application.Interfaces.Others;
using gamehub_API.Infrastructure.Models;
using Amazon.DynamoDBv2.Model;
using Amazon.DynamoDBv2.DocumentModel;


namespace gamehub_API.Infrastructure.Repositories
{
    public class VideogameRepository : IVideogameInterface
    {
        private readonly IDynamoDBContext _context;
        private readonly IAmazonDynamoDB _dynamoDBClient;

        public VideogameRepository(IDynamoDBContext dynamoDBContext, IAmazonDynamoDB amazonDynamoDB)
        {
            _context = dynamoDBContext;
            _dynamoDBClient = amazonDynamoDB;
        }

        public async Task<List<Videogame>> GetAllVideogamesAsync()
        {
            try
            {
                // Ejecutar la consulta de Cosmos DB para obtener todos los videojuegos
                var scanRequest = new Amazon.DynamoDBv2.Model.ScanRequest
                {
                    TableName = "Videogames"
                };

                // Enviar la solicitud de escaneo a DynamoDB 
                var response = await _dynamoDBClient.ScanAsync(scanRequest);

                // Verificar si se encontraron elementos
                if (response.Items == null || response.Items.Count == 0)
                {
                    return new List<Videogame>();
                }
                else
                {
                    return response.Items
                        .Select(item => _context.FromDocument<Videogame>(Amazon.DynamoDBv2.DocumentModel.Document.FromAttributeMap(item)))
                        .ToList();
                }

            }
            catch (Exception ex)
            {

                throw new Exception($"Error inesperado al obtener los videojuegos: {ex.Message}", ex);
            }

        }

        public async Task<Videogame> GetVideogameByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id), "El ID del videojuego no puede ser nulo o vacío.");
            }

            try
            {
                //ejecutar la consulta pasandole el id en cuestion para oibtener el videojuego
                var queryRequest = new QueryRequest
                {
                    TableName = "Videogames", // Nombre de la tabla en DynamoDB
                    IndexName = "id-index", // El nombre que le pongas al GSI
                    KeyConditionExpression = "id = :id", // Condición de la clave para buscar por ID
                    ExpressionAttributeValues = new Dictionary<string, AttributeValue>
                    {
                        { ":id", new AttributeValue { S = id } }
                    }
                };

                // Enviar la solicitud de consulta a DynamoDB
                var response = await _dynamoDBClient.QueryAsync(queryRequest);

                // Verificar si se encontraron elementos
                var videogame = response.Items.FirstOrDefault() != null
                    ? _context.FromDocument<Videogame>(Document.FromAttributeMap(response.Items.First()))
                    : null;

                if (videogame == null)
                {
                    throw new KeyNotFoundException($"No se encontró un videojuego con el ID: {id}");
                }

                return videogame;

            }
            catch (Exception ex)
            {

                throw new Exception($"Error inesperado al obtener el videojuego con ID {id}: {ex.Message}", ex);
            }

            
        }

    }

}
