namespace gamehub_API.Application.DTO.Comments
{
    public struct CreateCommentDTO
    {
        public string? userId { get; set; }
        public string? gameId { get; set; }
        public string? content { get; set; }
        public string? score { get; set; }
    }
}
