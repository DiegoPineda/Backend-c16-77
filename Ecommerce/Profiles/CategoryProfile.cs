using AutoMapper;
using Ecommerce.Entities;
using Ecommerce.Models.CategoryDto;

namespace Ecommerce.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryForCreationDto, Category>();
            CreateMap<CategoryForUpdateDto, Category>();
            CreateMap<Category, CategoryDto>();
        }
    }
}
