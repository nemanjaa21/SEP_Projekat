using AutoMapper;
using BankService.DTO;
using BankService.Models;

namespace BankService.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Card, CardInfoDTO>().ReverseMap();
        }
    }
}
