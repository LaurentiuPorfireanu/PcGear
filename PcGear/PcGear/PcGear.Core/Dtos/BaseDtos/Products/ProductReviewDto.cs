namespace PcGear.Core.Dtos.BaseDtos.Products;

public class ProductReviewDto
{
    public int Id { get; set; }
    public int Rating { get; set; }
    public string? ReviewText { get; set; }
    public string UserName { get; set; }
    public DateTime CreatedAt { get; set; }
}