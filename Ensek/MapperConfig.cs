using DAL;
using AutoMapper;
using Ensek.ViewModels;

namespace Ensek
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<MeterReadingViewModel, MeterReadingDTO>();
        }
    }
}
