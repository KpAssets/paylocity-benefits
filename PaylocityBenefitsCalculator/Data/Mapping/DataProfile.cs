using AutoMapper;
using Business.Employees;
using Business.Dependents;
using Data.Employees;
using Data.Dependents;

namespace Data.Mapping;

internal class DataProfile : Profile
{
    public DataProfile()
    {
        CreateMap<EmployeeEntity, Employee>()
            .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.id))
            .ForMember(dest => dest.FirstName, opts => opts.MapFrom(src => src.first_name))
            .ForMember(dest => dest.LastName, opts => opts.MapFrom(src => src.last_name))
            .ForMember(dest => dest.DateOfBirth, opts => opts.MapFrom(src => src.date_of_birth))
            .ForMember(dest => dest.Salary, opts => opts.MapFrom(src => src.salary))
            .ForMember(dest => dest.Dependents, opts => opts.MapFrom((src, dest) => src?.dependents ?? new List<DependentEntity>()))
            .ReverseMap();
        CreateMap<Dependent, DependentEntity>()
            .ForMember(dest => dest.id, opts => opts.MapFrom(src => src.Id))
            .ForMember(dest => dest.first_name, opts => opts.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.last_name, opts => opts.MapFrom(src => src.LastName))
            .ForMember(dest => dest.date_of_birth, opts => opts.MapFrom(src => src.DateOfBirth))
            .ForMember(dest => dest.relationship, opts => opts.MapFrom(src => src.Relationship.ToString().ToLower()));
        CreateMap<DependentEntity, Dependent>()
            .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.id))
            .ForMember(dest => dest.FirstName, opts => opts.MapFrom(src => src.first_name))
            .ForMember(dest => dest.LastName, opts => opts.MapFrom(src => src.last_name))
            .ForMember(dest => dest.DateOfBirth, opts => opts.MapFrom(src => src.date_of_birth))
            .ForMember(dest => dest.Relationship, opts => opts.MapFrom(src => Enum.Parse<Relationship>(src.relationship, true)));
    }
}