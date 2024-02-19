using AutoMapper;

namespace Ecommerce.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Entities.Product, Models.ProductDto>();
            CreateMap<Entities.Product, Models.ProductForListDto>();
            CreateMap<Models.ProductDto, Entities.Product>();
            CreateMap<Models.ProductForCreationDto, Entities.Product>();
        }
    }
}
