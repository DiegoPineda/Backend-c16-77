namespace Ecommerce.Models.ProductDto
{
    public class ProductForListDto
    {

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
