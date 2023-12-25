using AutoMapper;
using PaymentServiceProvider.Models;
using PaymentServiceProvider.Dto;

namespace PaymentServiceProvider.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Merchant, RegisterMerchantDTO>().ReverseMap();
            CreateMap<PaymentService, PaymentServiceDTO>().ReverseMap();
        }
    }
}
