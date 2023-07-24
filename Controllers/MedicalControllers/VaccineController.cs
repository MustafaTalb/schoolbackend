using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace firstapi.Controllers.MedicalControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VaccineController : ControllerBase
    {
        private readonly IMedicalService<Vaccine, AddVaccineDto> _vaccineService;

        public VaccineController(IMedicalService<Vaccine, AddVaccineDto> vaccineService)
        {
            _vaccineService = vaccineService;
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<Vaccine>>>> GetAll()
        {
            return Ok(await _vaccineService.GetAll());
        }

        [HttpGet("GetBy{id}")]
        public async Task<ActionResult<ServiceResponse<Vaccine>>> Get(int id)
        {
            return Ok(await _vaccineService.GetById(id));
        }

        [HttpPost("AddNew")]
        public async Task<ActionResult<ServiceResponse<List<Vaccine>>>> Add(AddVaccineDto vaccine)
        {
            return Ok(await _vaccineService.AddNew(vaccine));
        }

        [HttpPut("Update")]
        public async Task<ActionResult<ServiceResponse<Vaccine>>> Update(Vaccine updateVaccine)
        {
            var response = await _vaccineService.Update(updateVaccine);
            if (response.Data is null) return NotFound(response);
            return Ok(response);
        }

        [HttpDelete("DeleteBy{id}")]
        public async Task<ActionResult<ServiceResponse<Vaccine>>> Delete(int id)
        {
            return Ok(await _vaccineService.Delete(id));
        }
    }
}