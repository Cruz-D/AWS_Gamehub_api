using Amazon.DynamoDBv2.DataModel;
using System.ComponentModel.DataAnnotations;

namespace gamehub_API.Infrastructure.Models
{
    [DynamoDBTable("Videogames")]
    public class Videogame
    {
        [DynamoDBHashKey] // Partition Key
        [DynamoDBProperty("genre")]
        public string? Genre { get; set; }

        [DynamoDBProperty("id")]
        [DynamoDBGlobalSecondaryIndexHashKey("Id-index")] // Secondary Index for querying by Id
        public string? Id { get; set; }

        [DynamoDBProperty("title")]
        public string? Title { get; set; }

        [DynamoDBProperty("platform")]
        public string? Platform { get; set; }

        [DynamoDBProperty("rating")]
        public string? Rating { get; set; }

        [DynamoDBProperty("publisher")]
        public string? Publisher { get; set; }

        [DynamoDBProperty("release")]
        public string ReleaseDate { get; set; }

        [DynamoDBProperty("status")]
        public string? Status { get; set; }

        [DynamoDBProperty("image")]
        public string? Image { get; set; }
    }
}
