using AutoMapper;
using WebStarterDBApp.DTO;
using WebStarterDBApp.Models;

namespace WebStarterDBApp.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<StudentInsertDTO, Student>().ReverseMap();
            CreateMap<StudentUpdateDTO, Student>().ReverseMap();
            CreateMap<StudentReadOnlyDTO, Student>().ReverseMap();
        }
    }
}
