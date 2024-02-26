using AutoMapper;
using Ecommerce.Models.ProductDto;

namespace Ecommerce.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Entities.Product, ProductDto>();
            CreateMap<Entities.Product, ProductForListDto>();
            CreateMap<ProductDto, Entities.Product>();
            CreateMap<ProductForUpdateDto, Entities.Product>();
            CreateMap<ProductForCreationDto, Entities.Product>();
        }
    }
}
