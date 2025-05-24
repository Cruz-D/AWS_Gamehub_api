namespace gamehub_API.Application.DTO.Comments
{
    public struct ReadCommentDTO
    {
        public string? userId { get; set; } // Usuario que hizo el comentario
        public string? commentId { get; set; } // ID único del comentario
        public string? gameId { get; set; } // ID del videojuego comentado
        public string? content { get; set; } // Texto del comentario
        public string? score { get; set; } // Puntuación del comentario
        public string? createdAt { get; set; } // Fecha de creación
    }
}
