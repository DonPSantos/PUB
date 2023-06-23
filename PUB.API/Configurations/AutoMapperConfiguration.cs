using AutoMapper;
using PUB.API.V1.ViewModel;
using PUB.Domain.Entities;

namespace PUB.API.Configurations
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<OneDrinkPromo, RegisterOneDrinkPromo>().ReverseMap();
        }
    }
}