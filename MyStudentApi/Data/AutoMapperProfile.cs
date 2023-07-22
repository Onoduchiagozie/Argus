using AutoMapper;
using MyStudentApi.Models;
using MyStudentApi.Models.DTO;

namespace MyStudentApi.Data
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<StudentsDTO, Student>().ReverseMap();
            CreateMap<SchoolClassDTO, SchoolClass>().ReverseMap();
        }
    }
}
