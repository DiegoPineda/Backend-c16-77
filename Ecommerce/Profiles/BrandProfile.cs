using AutoMapper;
using Ecommerce.Entities;
using Ecommerce.Models.BrandDto;

namespace Ecommerce.Profiles
{
    public class BrandProfile : Profile
    {
        public BrandProfile()
        {
            CreateMap<BrandForCreationDto, Brand>();
            CreateMap<BrandForUpdateDto, Brand>();
            CreateMap<Brand, BrandDto>();
        }

    }
}
