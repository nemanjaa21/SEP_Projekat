using AgencyService.DTO;
using AgencyService.Models;
using AutoMapper;

namespace AgencyService.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ServiceOfferItem, ServiceOfferItemDto>().ReverseMap();
            CreateMap<ServiceOffer, ServiceOfferDto>().ReverseMap();
            CreateMap<PaymentService, PaymentServiceDto>().ReverseMap();
            CreateMap<Agency,  AgencyDto>().ReverseMap();
        }
    }
}
