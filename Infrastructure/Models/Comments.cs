using Amazon.DynamoDBv2.DataModel;

namespace gamehub_API.Infrastructure.Models
{
    [DynamoDBTable("Comments")]
    public class Comments
    {
        [DynamoDBHashKey] // Clave primaria (Partition Key)
        [DynamoDBProperty("userId")]
        public string? userId { get; set; } // Usuario que hace el comentario

        [DynamoDBRangeKey] // Clave de ordenación principal
        [DynamoDBProperty("commentId")]
        public string? commentId { get; set; } // ID único del comentario

        [DynamoDBProperty("gameId")]
        [DynamoDBGlobalSecondaryIndexHashKey("gameId-Index")] // LSI para buscar por videojuego
        public string? gameId { get; set; } // ID del videojuego comentado

        [DynamoDBProperty("content")]
        public string? content { get; set; } // Texto del comentario

        [DynamoDBProperty("score")]
        public string? score { get; set; } // Texto del comentario

        [DynamoDBProperty("createdAt")]
        public string? createdAt { get; set; } // Fecha de creación

        [DynamoDBProperty("updatedAt")]
        public string? updatedAt { get; set; } // Fecha de actualización

        [DynamoDBProperty("isEdited")]
        public bool? isEdited { get; set; }

        [DynamoDBProperty("isDeleted")]
        public bool? isDeleted { get; set; }
    }
}
