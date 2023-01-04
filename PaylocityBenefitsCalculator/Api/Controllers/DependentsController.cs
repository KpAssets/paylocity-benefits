using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Api.Models.Validations;
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
            ValidateDependentId(id);

            return Ok(
                _mapper.Map<GetDependentDto>(await _dependentService.GetAsync(id)));
        }

        [
            SwaggerOperation(Summary = "Get all dependents"),
            HttpGet
        ]
        public async Task<ActionResult<ApiResponse<List<GetDependentDto>>>> GetAll()
        {
            return Ok(_mapper.Map<List<GetDependentDto>>(await _dependentService.GetAsync()));
        }

        [
            SwaggerOperation(Summary = "Add dependent"),
            HttpPost
        ]
        public async Task<ActionResult<ApiResponse<GetDependentDto>>> AddDependent(AddDependentWithEmployeeIdDto newDependent)
        {
            ValidationHelper.Validate(newDependent);

            return Ok(new ApiResponse<GetDependentDto>
            {
                Data = _mapper.Map<GetDependentDto>(
                    await _dependentService.UpsertAsync(
                        _mapper.Map<Dependent>(newDependent)))
            });
        }

        [
            SwaggerOperation(Summary = "Update dependent"),
            HttpPut("{id}")
        ]
        public async Task<ActionResult<ApiResponse<GetDependentDto>>> UpdateDependent(uint id, UpdateDependentDto updatedDependent)
        {
            ValidateDependentId(id);

            ValidationHelper.Validate(updatedDependent);

            var dependent = _mapper.Map<Dependent>(updatedDependent);
            dependent.Id = id;

            return Ok(new ApiResponse<GetDependentDto>
            {
                Data = _mapper.Map<GetDependentDto>(
                    await _dependentService.UpsertAsync(dependent))
            });
        }

        [
            SwaggerOperation(Summary = "Delete dependent"),
            HttpDelete("{id}")
        ]
        public async Task<ActionResult<ApiResponse<GetDependentDto>>> DeleteDependent(uint id)
        {
            ValidateDependentId(id);

            return Ok(new ApiResponse<GetDependentDto>
            {
                Data = _mapper.Map<GetDependentDto>(await _dependentService.DeleteAsync(id))
            });
        }

        private void ValidateDependentId(uint id)
        {
            if (id <= 0)
                throw new InvalidDataException("Dependent id must be greater than 0");
        }
    }
}
