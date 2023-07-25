using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace firstapi.Controllers.FamilyControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FamilyController : ControllerBase
    {
        private readonly IFamilyService<Family, AddFamilyDto> _familyService;
        public FamilyController(IFamilyService<Family, AddFamilyDto> familyService)
        {
            _familyService = familyService;
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<Family>>>> GetAll()
        {
            return Ok(await _familyService.GetAll());
        }

        [HttpGet("GetBy{id}")]
        public async Task<ActionResult<ServiceResponse<Family>>> GetById(int id)
        {
            return Ok(await _familyService.GetById(id));
        }

        [HttpPost("AddNew")]
        public async Task<ActionResult<ServiceResponse<Family>>> Add(AddFamilyDto family)
        {
            return Ok(await _familyService.AddNew(family));
        }

        [HttpPut("Update")]
        public async Task<ActionResult<ServiceResponse<Family>>> Update(Family updatedFamily)
        {
            var response = await _familyService.Update(updatedFamily);
            if (response.Data is null) return NotFound(response);
            return Ok(response);
        }

        [HttpDelete("DeleteBy{id}")]
        public async Task<ActionResult<ServiceResponse<Family>>> Delete(int id)
        {
            return Ok(await _familyService.Delete(id));
        }
    }
}