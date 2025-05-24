using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using gamehub_API.Application.Interfaces;
using gamehub_API.Application.Interfaces.Others;
using gamehub_API.Infrastructure.Models;
using System.Text.Json;

namespace gamehub_API.Infrastructure.Repositories
{
    public class UserRepository : IUserInterface
    {
        private readonly IDynamoDBContext _context;
        private readonly IAmazonDynamoDB _dynamoDBClient;

        public UserRepository(IDynamoDBContext dynamoDBContext, IAmazonDynamoDB amazonDynamoDB)
        {
            _context = dynamoDBContext;
            _dynamoDBClient = amazonDynamoDB;
        }

        public async Task<Users> AddUserAsync(Users user)
        {
            try
            {
                // Guardar el usuario en la base de datos  
                await _context.SaveAsync(user);

                // Devolver el usuario creado para mapearlo en un DTO  
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error inesperado: {ex.Message}", ex);
            }
        }

        public async Task<Users> GetUserByIdAsync(string userId)
        {

            // Verificar si el userId es nulo o vacío
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException(nameof(userId), "El ID del usuario no puede ser nulo o vacío.");
            }
            try
            {

                return await _context.LoadAsync<Users>(userId);
            }
           
            catch (Exception ex)
            {
                throw new Exception($"Error inesperado: {ex.Message}", ex);
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
                // Actualizar los cambios del usuario en DynamoDB  
                await _context.SaveAsync(user);

                // Devolver el usuario actualizado  
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en DynamoDB: {ex.Message}", ex);
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
                // Eliminar el usuario de la base de datos
                await _context.DeleteAsync(user);

                return user;
            }

            catch (Exception ex)
            {
                throw new Exception($"Error en Cosmos DB: {ex.Message}", ex);
            }
        }

        public async Task<Users> UpdatePasswordUserAsync(Users user)
        {
            throw new NotImplementedException();
        }

    }
}
