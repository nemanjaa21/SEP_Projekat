using AuthService.DTO;
using AuthService.Models;
using AutoMapper;

namespace AuthService.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserLoginDTO>().ReverseMap();
        }
    }
}
