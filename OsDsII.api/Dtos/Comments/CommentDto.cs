namespace OsDsII.api.Dtos
{
    public record CommentDto(long Id, string Description, DateTimeOffset SendDate, int ServiceOrderId);
    
}