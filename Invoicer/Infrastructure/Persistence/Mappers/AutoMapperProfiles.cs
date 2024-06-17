using AutoMapper;
using Domain.DTOs.Response;
using Infrastructure.Persistence.Mappers.Entities;

namespace Infrastructure.Persistence.Mappers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<InvoiceEntity, Invoice>().ReverseMap();
            CreateMap<ProductEntity, Product>();
        }
    }
}
