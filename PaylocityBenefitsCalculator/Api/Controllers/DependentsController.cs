using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Api.Models.Dependents;
using Api.Models;
using Business.Dependents;

namespace Api.Controllers
{
    [
        ApiController,
        Route("api/v1/[controller]")
    ]
    public class DependentsController : ControllerBase
    {
        private readonly IDependentService _dependentService;
        private readonly IMapper _mapper;

        public DependentsController(
            IDependentService dependentService,
            IMapper mapper)
        {
            _dependentService = dependentService;
            _mapper = mapper;
        }

        [
            SwaggerOperation(Summary = "Get dependent by id"),
            HttpGet("{id}")
        ]
        public async Task<ActionResult<ApiResponse<GetDependentDto>>> Get(uint id)
        {
            var dependent = await _dependentService.GetAsync(id);

            return Ok(new ApiResponse<GetDependentDto>
            {
                Data = _mapper.Map<GetDependentDto>(dependent)
            });
        }

        [
            SwaggerOperation(Summary = "Get all dependents"),
            HttpGet
        ]
        public async Task<ActionResult<ApiResponse<List<GetDependentDto>>>> GetAll()
        {
            throw new NotImplementedException();
        }

        [
            SwaggerOperation(Summary = "Add dependent"),
            HttpPost
        ]
        public async Task<ActionResult<ApiResponse<List<AddDependentWithEmployeeIdDto>>>> AddDependent(AddDependentWithEmployeeIdDto newDependent)
        {
            throw new NotImplementedException();
        }

        [
            SwaggerOperation(Summary = "Update dependent"),
            HttpPut("{id}")
        ]
        public async Task<ActionResult<ApiResponse<GetDependentDto>>> UpdateDependent(int id, UpdateDependentDto updatedDependent)
        {
            throw new NotImplementedException();
        }

        [
            SwaggerOperation(Summary = "Delete dependent"),
            HttpDelete("{id}")
        ]
        public async Task<ActionResult<ApiResponse<List<GetDependentDto>>>> DeleteDependent(int id)
        {
            throw new NotImplementedException();
        }
    }
}
