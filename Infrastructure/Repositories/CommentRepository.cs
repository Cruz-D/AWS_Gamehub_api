using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using gamehub_API.Application.Interfaces;
using gamehub_API.Infrastructure.Models;

namespace gamehub_API.Infrastructure.Repositories
{
    public class CommentRepository : ICommentsInterface
    {
        private readonly IDynamoDBContext _context;
        private readonly IAmazonDynamoDB _dynamoDBClient;

        public CommentRepository(IDynamoDBContext context, IAmazonDynamoDB dynamoDBClient)
        {
            _context = context;
            _dynamoDBClient = dynamoDBClient;
        }

        public async Task<Comments> AddCommentAsync(Comments comment)
        {
            await _context.SaveAsync(comment);
            return comment;
        }

        public async Task<IEnumerable<Comments>> GetCommentsByGameAsync(string gameId, int pageNumber, int pageSize)
        {
            var request = new QueryRequest
            {
                TableName = "Comments",
                IndexName = "gameId-index",
                KeyConditionExpression = "gameId = :gameId",
                FilterExpression = "isDeleted = :isDeleted",
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>
                {
                    { ":gameId", new AttributeValue { S = gameId } },
                    { ":isDeleted", new AttributeValue { N = "0" } }
                },
                Limit = pageSize,
                ExclusiveStartKey = null // Implementa paginación si lo necesitas
            };

            var response = await _dynamoDBClient.QueryAsync(request);
            var comments = response.Items.Select(item => _context.FromDocument<Comments>(Document.FromAttributeMap(item)));
            return comments;
        }

        public async Task<Comments> EditCommentAsync(string userId, string commentId, string content, string score)
        {
            var comment = await _context.LoadAsync<Comments>(userId, commentId);
            if (comment == null) throw new Exception("Comentario no encontrado");

            comment.content = content;
            comment.score = score;
            comment.isEdited = true;
            comment.updatedAt = DateTime.UtcNow.ToString("o");

            await _context.SaveAsync(comment);
            return comment;
        }

        public async Task DeleteCommentAsync(string userId, string commentId)
        {
            var comment = await _context.LoadAsync<Comments>(userId, commentId);
            if (comment == null) throw new Exception("Comentario no encontrado");

            comment.isDeleted = true;
            comment.updatedAt = DateTime.UtcNow.ToString("o");

            await _context.SaveAsync(comment);
        }

        public async Task<double> GetAverageScoreByGameAsync(string gameId)
        {
            var request = new QueryRequest
            {
                TableName = "Comments",
                IndexName = "GameIndex", // LSI por gameId
                KeyConditionExpression = "gameId = :gameId",
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>
        {
            { ":gameId", new AttributeValue { S = gameId } }
        }
            };

            var response = await _dynamoDBClient.QueryAsync(request);
            var scores = response.Items
                .Select(item => int.TryParse(item["score"].N ?? item["score"].S, out var s) ? s : (int?)null)
                .Where(s => s.HasValue)
                .Select(s => s.Value)
                .ToList();

            return scores.Count > 0 ? scores.Average() : 0.0;
        }
    }
}
