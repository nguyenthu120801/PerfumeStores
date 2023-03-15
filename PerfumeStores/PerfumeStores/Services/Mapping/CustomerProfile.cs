using AutoMapper;
using PerfumeStores.Core.DTOs;

namespace PerfumeStores.Services.Mapping
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            //CreateMap<RegisterDTO, Customer>();
            CreateMap<RegisterDTO, LoginDTO>();
        }
    }
}
