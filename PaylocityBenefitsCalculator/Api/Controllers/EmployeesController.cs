using Api.Models.Dependents;
using Api.Models.Employees;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Business.Employees;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [SwaggerOperation(Summary = "Get employee by id")]
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<GetEmployeeDto>>> Get(uint id)
        {
            await _employeeService.GetAsync(id);

            return Ok(new ApiResponse<GetEmployeeDto>());
        }

        [SwaggerOperation(Summary = "Get all employees")]
        [HttpGet("")]
        public async Task<ActionResult<ApiResponse<List<GetEmployeeDto>>>> GetAll()
        {
            await _employeeService.GetAsync();
            //task: use a more realistic production approach
            var employees = new List<GetEmployeeDto>
            {
                new()
                {
                    Id = 1,
                    FirstName = "LeBron",
                    LastName = "James",
                    Salary = 75420.99m,
                    DateOfBirth = new DateTime(1984, 12, 30)
                },
                new()
                {
                    Id = 2,
                    FirstName = "Ja",
                    LastName = "Morant",
                    Salary = 92365.22m,
                    DateOfBirth = new DateTime(1999, 8, 10),
                    Dependents = new List<GetDependentDto>
                    {
                        new()
                        {
                            Id = 1,
                            FirstName = "Spouse",
                            LastName = "Morant",
                            Relationship = "Spouse",
                            DateOfBirth = new DateTime(1998, 3, 3)
                        },
                        new()
                        {
                            Id = 2,
                            FirstName = "Child1",
                            LastName = "Morant",
                            Relationship = "Child",
                            DateOfBirth = new DateTime(2020, 6, 23)
                        },
                        new()
                        {
                            Id = 3,
                            FirstName = "Child2",
                            LastName = "Morant",
                            Relationship = "Child",
                            DateOfBirth = new DateTime(2021, 5, 18)
                        }
                    }
                },
                new()
                {
                    Id = 3,
                    FirstName = "Michael",
                    LastName = "Jordan",
                    Salary = 143211.12m,
                    DateOfBirth = new DateTime(1963, 2, 17),
                    Dependents = new List<GetDependentDto>
                    {
                        new()
                        {
                            Id = 4,
                            FirstName = "DP",
                            LastName = "Jordan",
                            Relationship = "DomesticPartner",
                            DateOfBirth = new DateTime(1974, 1, 2)
                        }
                    }
                }
            };

            var result = new ApiResponse<List<GetEmployeeDto>>
            {
                Data = employees,
                Success = true
            };

            return result;
        }

        [SwaggerOperation(Summary = "Add employee")]
        [HttpPost]
        public async Task<ActionResult<ApiResponse<List<AddEmployeeDto>>>> AddEmployee(AddEmployeeDto newEmployee)
        {
            await _employeeService.UpsertAsync();

            return Ok(new ApiResponse<List<AddEmployeeDto>>());
        }

        [SwaggerOperation(Summary = "Update employee")]
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<GetEmployeeDto>>> UpdateEmployee(int id, UpdateEmployeeDto updatedEmployee)
        {
            await _employeeService.UpsertAsync();

            return Ok(new ApiResponse<GetEmployeeDto>());
        }

        [SwaggerOperation(Summary = "Delete employee")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<List<GetEmployeeDto>>>> DeleteEmployee(int id)
        {
            await _employeeService.DeleteAsync();

            return Ok(new ApiResponse<List<GetEmployeeDto>>());
        }
    }
}
