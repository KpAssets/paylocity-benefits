using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Api.Models.Validations;
using Api.Models.Employees;
using Api.Models;
using Business.Employees;

namespace Api.Controllers
{
    [
        ApiController,
        Route("api/v1/[controller]")
    ]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;

        public EmployeesController(
            IEmployeeService employeeService,
            IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }

        [
            SwaggerOperation(Summary = "Get employee by id"),
            HttpGet("{id}")
        ]
        public async Task<ActionResult<ApiResponse<GetEmployeeDto>>> Get(uint id)
        {
            ValidateEmployeeId(id);

            return Ok(new ApiResponse<GetEmployeeDto>
            {
                Data = _mapper.Map<GetEmployeeDto>(await _employeeService.GetAsync(id))
            });
        }

        [
            SwaggerOperation(Summary = "Get all employees"),
            HttpGet
        ]
        public async Task<ActionResult<ApiResponse<List<GetEmployeeDto>>>> GetAll()
        {
            return Ok(new ApiResponse<List<GetEmployeeDto>>
            {
                Data = _mapper.Map<List<GetEmployeeDto>>(await _employeeService.GetAsync()),
                Success = true
            });
        }

        [
            SwaggerOperation(Summary = "Add employee"),
            HttpPost
        ]
        public async Task<ActionResult<ApiResponse<GetEmployeeDto>>> AddEmployee(AddEmployeeDto newEmployee)
        {
            ValidationHelper.Validate(newEmployee);

            return Ok(new ApiResponse<GetEmployeeDto>
            {
                Data = _mapper.Map<GetEmployeeDto>(
                    await _employeeService.UpsertAsync(
                        _mapper.Map<Employee>(newEmployee)))
            });
        }

        [
            SwaggerOperation(Summary = "Update employee"),
            HttpPut("{id}")
        ]
        public async Task<ActionResult<ApiResponse<GetEmployeeDto>>> UpdateEmployee(uint id, UpdateEmployeeDto updatedEmployee)
        {
            ValidateEmployeeId(id);

            ValidationHelper.Validate(updatedEmployee);

            var employee = _mapper.Map<Employee>(updatedEmployee);
            employee.Id = id;

            return Ok(new ApiResponse<GetEmployeeDto>
            {
                Data = _mapper.Map<GetEmployeeDto>(
                    await _employeeService.UpsertAsync(employee))
            });
        }

        [
            SwaggerOperation(Summary = "Delete employee"),
            HttpDelete("{id}")
        ]
        public async Task<ActionResult<ApiResponse<GetEmployeeDto>>> DeleteEmployee(uint id)
        {
            ValidateEmployeeId(id);

            return Ok(new ApiResponse<GetEmployeeDto>
            {
                Data = _mapper.Map<GetEmployeeDto>(await _employeeService.DeleteAsync(id))
            });
        }

        private void ValidateEmployeeId(uint id)
        {
            if (id <= 0)
                throw new InvalidDataException("Employee id must be greater than 0");
        }
    }
}
