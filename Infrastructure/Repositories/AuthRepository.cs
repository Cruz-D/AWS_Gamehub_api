using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;

using gamehub_API.Application.Interfaces.Others;
using Microsoft.IdentityModel.Tokens;

namespace gamehub_API.Infrastructure.Repositories
{
    public class AuthRepository : IAuthInterface
    {
        private readonly IDynamoDBContext _context;
        private readonly IAmazonDynamoDB _dynamoDBClient;

        public AuthRepository(IDynamoDBContext context, IAmazonDynamoDB dynamoDBClient)
        {
            _context = context;
            _dynamoDBClient = dynamoDBClient;
        }

        public async Task<Users> GetUserByUsernameOrEmailAsync(string usernameOrEmail)
        {
            try
            {
                var scanRequest = new ScanRequest
                {
                    TableName = "Users",
                    FilterExpression = "systemInfo.username = :usernameOrEmail OR systemInfo.email = :usernameOrEmail",
                    ExpressionAttributeValues = new Dictionary<string, AttributeValue>
            {
                { ":usernameOrEmail", new AttributeValue { S = usernameOrEmail } }
            }
                };

                var response = await _dynamoDBClient.ScanAsync(scanRequest);

                if (response.Items != null && response.Items.Count > 0)
                {
                    return MapUser(response.Items[0]);
                }

                throw new KeyNotFoundException("Usuario no encontrado.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error ejecutando el escaneo: {ex.Message}");
                throw new Exception("Error al obtener el usuario.", ex);
            }
        }



        private Users MapUser(Dictionary<string, AttributeValue> item)
        {
            return new Users
            {
                userId = item["userId"].S,
                systemInfo = new SystemInfo
                {
                    username = item["systemInfo"].M["username"].S,
                    email = item["systemInfo"].M["email"].S,
                    password = item["systemInfo"].M["password"].S
                },
                authentication = new Authentication
                {
                    isAuthenticated = item["authentication"].M["isAuthenticated"].BOOL,
                    refreshToken = item["authentication"].M["refreshToken"].S,
                    accessToken = item["authentication"].M["accessToken"].S,
                    tokenExpiry = item["authentication"].M["tokenExpiry"].S,
                    tokenCreatedAt = item["authentication"].M["tokenCreatedAt"].S
                },
                timestamps = new Timestamps
                {
                    lastLogin = item["timestamps"].M["lastLogin"].S
                }
            };
        }
    }

}
