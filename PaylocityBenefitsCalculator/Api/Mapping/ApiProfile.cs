using AutoMapper;
using Api.Models.Employees;
using Business.Employees;

namespace Api.Mapping;

internal sealed class ApiProfile : Profile
{
    public ApiProfile()
    {
        CreateMap<Employee, GetEmployeeDto>()
            .ForMember(x => x.Id, opts => opts.MapFrom(src => src.Id))
            .ForMember(x => x.FirstName, opts => opts.MapFrom(src => src.FirstName))
            .ForMember(x => x.LastName, opts => opts.MapFrom(src => src.LastName))
            .ForMember(x => x.DateOfBirth, opts => opts.MapFrom(src => src.DateOfBirth))
            .ForMember(x => x.Salary, opts => opts.MapFrom(src => src.Salary));
        CreateMap<AddEmployeeDto, Employee>()
            .ForMember(dest => dest.FirstName, opts => opts.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opts => opts.MapFrom(src => src.LastName))
            .ForMember(dest => dest.DateOfBirth, opts => opts.MapFrom(src => src.DateOfBirth))
            .ForMember(dest => dest.Salary, opts => opts.MapFrom(src => src.Salary));
        CreateMap<UpdateEmployeeDto, Employee>()
            .ForMember(dest => dest.FirstName, opts => opts.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opts => opts.MapFrom(src => src.LastName))
            .ForMember(dest => dest.DateOfBirth, opts => opts.MapFrom(src => src.DateOfBirth))
            .ForMember(dest => dest.Salary, opts => opts.MapFrom(src => src.Salary));
    }
}