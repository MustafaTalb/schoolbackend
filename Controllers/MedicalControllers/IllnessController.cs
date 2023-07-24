using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace firstapi.Controllers.MedicalControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IllnessController : ControllerBase
    {
        private readonly IMedicalService<Illness, AddIllnessDto> _illnessService;

        public IllnessController(IMedicalService<Illness, AddIllnessDto> illnessService)
        {
            _illnessService = illnessService;
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<Illness>>>> GetAll()
        {
            return Ok(await _illnessService.GetAll());
        }

        [HttpGet("GetBy{id}")]
        public async Task<ActionResult<ServiceResponse<Illness>>> Get(int id)
        {
            return Ok(await _illnessService.GetById(id));
        }

        [HttpPost("AddNew")]
        public async Task<ActionResult<ServiceResponse<List<Illness>>>> Add(AddIllnessDto illness)
        {
            return Ok(await _illnessService.AddNew(illness));
        }

        [HttpPut("Update")]
        public async Task<ActionResult<ServiceResponse<Illness>>> Update(Illness updateIllness)
        {
            var response = await _illnessService.Update(updateIllness);
            if (response.Data is null) return NotFound(response);
            return Ok(response);
        }

        [HttpDelete("DeleteBy{id}")]
        public async Task<ActionResult<ServiceResponse<Illness>>> Delete(int id)
        {
            return Ok(await _illnessService.Delete(id));
        }
    }
}