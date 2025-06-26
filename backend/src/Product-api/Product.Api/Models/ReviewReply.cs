namespace Products.Api.Models;

public class ReviewReply : BaseEntity
{
   public Guid ReviewId { get; set; }
   public Review Review { get; set; }
   public Guid UserId { get; set; } 
   public string Message { get; set; }
}
