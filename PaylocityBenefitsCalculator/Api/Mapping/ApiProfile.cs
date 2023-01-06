using AutoMapper;
using Api.Models.Dependents;
using Api.Models.Employees;
using Api.Models.Payroll;
using Business.Dependents;
using Business.Employees;
using Business.Payroll;

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
            .ForMember(dest => dest.Dependents, opts => opts.MapFrom((src, _) => src?.Dependents ?? new List<Dependent>()));
        CreateMap<AddEmployeeDto, Employee>()
            .ForMember(dest => dest.FirstName, opts => opts.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opts => opts.MapFrom(src => src.LastName))
            .ForMember(dest => dest.DateOfBirth, opts => opts.MapFrom(src => src.DateOfBirth))
            .ForMember(dest => dest.Salary, opts => opts.MapFrom(src => src.Salary))
            .ForMember(dest => dest.Dependents, opts => opts.MapFrom((src, _) => src?.Dependents ?? new List<AddDependentDto>()));
        CreateMap<UpdateEmployeeDto, Employee>()
            .ForMember(dest => dest.FirstName, opts => opts.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opts => opts.MapFrom(src => src.LastName))
            .ForMember(dest => dest.DateOfBirth, opts => opts.MapFrom(src => src.DateOfBirth))
            .ForMember(dest => dest.Salary, opts => opts.MapFrom(src => src.Salary));
        CreateMap<Dependent, GetDependentDto>()
            .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
            .ForMember(dest => dest.FirstName, opts => opts.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opts => opts.MapFrom(src => src.LastName))
            .ForMember(dest => dest.DateOfBirth, opts => opts.MapFrom(src => src.DateOfBirth))
            .ForMember(dest => dest.Relationship, opts => opts.MapFrom(src => src.Relationship.ToString()));
        CreateMap<AddDependentDto, Dependent>()
            .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
            .ForMember(dest => dest.FirstName, opts => opts.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opts => opts.MapFrom(src => src.LastName))
            .ForMember(dest => dest.DateOfBirth, opts => opts.MapFrom(src => src.DateOfBirth))
            .ForMember(dest => dest.Relationship, opts => opts.MapFrom((src, _) => Enum.Parse<Relationship>(src?.Relationship ?? "none", true)));
        CreateMap<AddDependentWithEmployeeIdDto, Dependent>()
            .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
            .ForMember(dest => dest.FirstName, opts => opts.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opts => opts.MapFrom(src => src.LastName))
            .ForMember(dest => dest.DateOfBirth, opts => opts.MapFrom(src => src.DateOfBirth))
            .ForMember(dest => dest.Relationship, opts => opts.MapFrom((src, _) => Enum.Parse<Relationship>(src?.Relationship ?? "none", true)))
            .ForMember(dest => dest.EmployeeId, opts => opts.MapFrom(src => src.EmployeeId));
        CreateMap<UpdateDependentDto, Dependent>()
            .ForMember(dest => dest.FirstName, opts => opts.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opts => opts.MapFrom(src => src.LastName))
            .ForMember(dest => dest.DateOfBirth, opts => opts.MapFrom(src => src.DateOfBirth))
            .ForMember(dest => dest.Relationship, opts => opts.MapFrom((src, _) => Enum.Parse<Relationship>(src?.Relationship ?? "none", true)));
        CreateMap<PayrollItem, PayrollItemDto>()
            .ForMember(dest => dest.Amount, opts => opts.MapFrom(src => src.Amount))
            .ForMember(dest => dest.Description, opts => opts.MapFrom(src => src.Description));
        CreateMap<Paycheck, PaycheckDto>()
            .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
            .ForMember(dest => dest.Employee, opts => opts.MapFrom(src => src.Employee))
            .ForMember(dest => dest.Earnings, opts => opts.MapFrom(src => src.Earnings))
            .ForMember(dest => dest.Deductions, opts => opts.MapFrom(src => src.Deductions))
            .ForMember(dest => dest.NetPay, opts => opts.MapFrom(src => src.NetPay));
    }
}