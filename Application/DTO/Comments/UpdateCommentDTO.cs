namespace gamehub_API.Application.DTO.Comments
{
    public class UpdateCommentDTO
    {
        public string? commentId { get; set; } // ID único del comentario
        public string? content { get; set; } // Texto del comentario
        public string? score { get; set; } // Puntuación del comentario
        public string? updatedAt { get; set; } // Fecha de actualización
    }
}
