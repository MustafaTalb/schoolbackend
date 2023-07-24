using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace firstapi.Controllers.MedicalControllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class StudentIllnessController : ControllerBase
    {
        private readonly IMedicalService<StudentIllness, AddStudentIllness> _studentIllnessService;

        public StudentIllnessController(IMedicalService<StudentIllness, AddStudentIllness> studentIllnessService)
        {
            _studentIllnessService = studentIllnessService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<StudentIllness>>>> GetAll()
        {
            return Ok(await _studentIllnessService.GetAll());
        }

        [HttpGet("GetBy{id}")]
        public async Task<ActionResult<ServiceResponse<StudentIllness>>> Get(int id)
        {
            return Ok(await _studentIllnessService.GetById(id));
        }

        [HttpPost("AddNew")]
        public async Task<ActionResult<ServiceResponse<List<StudentIllness>>>> Add(AddStudentIllness illness)
        {
            return Ok(await _studentIllnessService.AddNew(illness));
        }

        [HttpPut("Update")]
        public async Task<ActionResult<ServiceResponse<StudentIllness>>> Update(StudentIllness updateIllness)
        {
            var response = await _studentIllnessService.Update(updateIllness);
            if (response.Data is null) return NotFound(response);
            return Ok(response);
        }

        [HttpDelete("DeleteBy{id}")]
        public async Task<ActionResult<ServiceResponse<StudentIllness>>> Delete(int id)
        {
            return Ok(await _studentIllnessService.Delete(id));
        }
    }
}