namespace Ecommerce.Models.ProductDto
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public int Stock { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public bool Paused { get; set; }

        //adm ABM Productos, aumentar o disminuir stock
        //pausar o reactivar publicacion, agregar categorias y marcas
        //aumentar el precio de la totalidad de los productos en base a un %

        //public List<ProductColorDto> Colors { get; set; } = new List<ProductColorDto>();

    }
}
