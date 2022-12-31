using AutoMapper;
using Api.Models.Dependents;
using Api.Models.Employees;
using Business.Dependents;
using Business.Employees;

namespace Api.Mapping;

internal sealed class ApiProfile : Profile
{
    public ApiProfile()
    {
        CreateMap<Employee, GetEmployeeDto>()
            .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
            .ForMember(dest => dest.FirstName, opts => opts.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opts => opts.MapFrom(src => src.LastName))
            .ForMember(dest => dest.DateOfBirth, opts => opts.MapFrom(src => src.DateOfBirth))
            .ForMember(dest => dest.Salary, opts => opts.MapFrom(src => src.Salary))
            .ForMember(dest => dest.Dependents, opts => opts.MapFrom((src, dest) => src?.Dependents ?? new List<Dependent>()));
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
        CreateMap<Dependent, GetDependentDto>()
            .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
            .ForMember(dest => dest.FirstName, opts => opts.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opts => opts.MapFrom(src => src.LastName))
            .ForMember(dest => dest.DateOfBirth, opts => opts.MapFrom(src => src.DateOfBirth));
    }
}