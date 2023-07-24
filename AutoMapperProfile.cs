using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace firstapi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Student, GetStudentDto>();
            CreateMap<AddStudentDto, Student>();
            CreateMap<AddCityDto, City>();
            CreateMap<AddAreaDto, Area>();
            CreateMap<AddStreetDto, Street>();
            CreateMap<AddAddressDto, Address>();
            CreateMap<AddIllnessDto, Illness>();
            CreateMap<AddVaccineDto, Vaccine>();
            CreateMap<AddTakenVaccine, TakenVaccine>();
            CreateMap<AddStudentIllness, StudentIllness>();
        }
    }
}