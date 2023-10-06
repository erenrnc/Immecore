using AutoMapper;
using AutoMapper.Configuration.Conventions;
using Core.Db;
using Core.Models;

namespace Core.Mappers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<People, PeopleDto>();
            CreateMap<PeopleDto, People>();

            CreateMap<Salary, SalaryDto>();
            CreateMap<SalaryDto, Salary>();

            CreateMap<Department, DepartmentDto>();
            CreateMap<DepartmentDto, Department>();
        }
    }
}
