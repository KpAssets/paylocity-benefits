using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Api.Models;
using Api.Models.Payroll;
using Business.Payroll;

namespace Api.Controllers
{
    [
        ApiController,
        Route("api/v1/[controller]")
    ]
    public class PayrollController : ControllerBase
    {
        private readonly IPayrollService _payrollService;
        private readonly IMapper _mapper;

        public PayrollController(
            IPayrollService employeeService,
            IMapper mapper)
        {
            _payrollService = employeeService;
            _mapper = mapper;
        }

        [
            SwaggerOperation(Summary = "Calculate an employee's payroll"),
            HttpPost("{id}")
        ]
        public async Task<ActionResult<ApiResponse<PaycheckDto>>> CalculateEmployeesPayroll(uint id)
        {
            ValidateEmployeeId(id);

            return Ok(new ApiResponse<PaycheckDto>
            {
                Data = _mapper.Map<PaycheckDto>(await _payrollService.CalculatePaycheckForEmployee(id))
            });
        }

        private void ValidateEmployeeId(uint id)
        {
            if (id <= 0)
                throw new InvalidDataException("Employee id must be greater than 0");
        }
    }
}
