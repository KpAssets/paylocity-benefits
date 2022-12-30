using AutoMapper;
using Business.Employees;
using Data.Employees;

namespace Data.Mapping;

internal class DataProfile : Profile
{
    public DataProfile()
    {
        CreateMap<EmployeeEntity, Employee>()
            .ForMember(x => x.Id, opts => opts.MapFrom(src => src.id))
            .ForMember(x => x.FirstName, opts => opts.MapFrom(src => src.first_name))
            .ForMember(x => x.LastName, opts => opts.MapFrom(src => src.last_name))
            .ForMember(x => x.DateOfBirth, opts => opts.MapFrom(src => src.date_of_birth))
            .ForMember(x => x.Salary, opts => opts.MapFrom(src => src.salary));
    }
}