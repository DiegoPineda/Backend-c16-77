using AutoMapper;
using Ecommerce.Models.UsersDto;

namespace Ecommerce.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<Entities.Users, UsuarioDto>();
            CreateMap<UserForCreationDto, Entities.Users>();
            CreateMap<UserForUpdateDto, Entities.Users>();
        }
    }
}
