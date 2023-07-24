using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace firstapi.Controllers.MedicalControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TakenVaccineController : ControllerBase
    {
        private readonly IMedicalService<TakenVaccine, AddTakenVaccine> _takenVaccineService;

        public TakenVaccineController(IMedicalService<TakenVaccine, AddTakenVaccine> takenVaccineService)
        {
            _takenVaccineService = takenVaccineService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<TakenVaccine>>>> GetAll()
        {
            return Ok(await _takenVaccineService.GetAll());
        }

        [HttpGet("GetBy{id}")]
        public async Task<ActionResult<ServiceResponse<TakenVaccine>>> Get(int id)
        {
            return Ok(await _takenVaccineService.GetById(id));
        }

        [HttpPost("AddNew")]
        public async Task<ActionResult<ServiceResponse<List<TakenVaccine>>>> Add(AddTakenVaccine vaccine)
        {
            return Ok(await _takenVaccineService.AddNew(vaccine));
        }

        [HttpPut("Update")]
        public async Task<ActionResult<ServiceResponse<TakenVaccine>>> Update(TakenVaccine updatedVaccine)
        {
            var response = await _takenVaccineService.Update(updatedVaccine);
            if (response.Data is null) return NotFound(response);
            return Ok(response);
        }

        [HttpDelete("DeleteBy{id}")]
        public async Task<ActionResult<ServiceResponse<StudentIllness>>> Delete(int id)
        {
            return Ok(await _takenVaccineService.Delete(id));
        }
    }
}