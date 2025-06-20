namespace PcGear.Core.Dtos.Requests
{
    public class AddProductReviewRequest
    {
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int Rating { get; set; } 
        public string? ReviewText { get; set; }
    }
}
