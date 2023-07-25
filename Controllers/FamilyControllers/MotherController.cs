using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace firstapi.Controllers.FamilyControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MotherController : ControllerBase
    {
        private readonly IFamilyService<Mother, AddMotherDto> _motherService;
        public MotherController(IFamilyService<Mother, AddMotherDto> motherService)
        {
            _motherService = motherService;
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<Mother>>>> GetAll()
        {
            return Ok(await _motherService.GetAll());
        }

        [HttpGet("GetBy{id}")]
        public async Task<ActionResult<ServiceResponse<Mother>>> GetById(int id)
        {
            return Ok(await _motherService.GetById(id));
        }

        [HttpPost("AddNew")]
        public async Task<ActionResult<ServiceResponse<Mother>>> Add(AddMotherDto mother)
        {
            return Ok(await _motherService.AddNew(mother));
        }

        [HttpPut("Update")]
        public async Task<ActionResult<ServiceResponse<Mother>>> Update(Mother updatedMother)
        {
            var response = await _motherService.Update(updatedMother);
            if (response.Data is null) return NotFound(response);
            return Ok(response);
        }

        [HttpDelete("DeleteBy{id}")]
        public async Task<ActionResult<ServiceResponse<Mother>>> Delete(int id)
        {
            return Ok(await _motherService.Delete(id));
        }
    }
}