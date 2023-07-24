using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace firstapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetStudentDto>>>> GetAll()
        {
            return Ok(await _studentService.GetAll());
        }

        [HttpGet("GetBy{id}")]
        public async Task<ActionResult<ServiceResponse<GetStudentDto>>> Get(int id)
        {
            return Ok(await _studentService.GetById(id));
        }

        [HttpPost("AddNew")]
        public async Task<ActionResult<ServiceResponse<List<GetStudentDto>>>> Add(AddStudentDto student)
        {
            return Ok(await _studentService.AddNew(student));
        }

        [HttpPut("Update")]
        public async Task<ActionResult<ServiceResponse<GetStudentDto>>> Update(UpdateStudentDto updateStudent)
        {
            var response = await _studentService.Update(updateStudent);
            if (response.Data is null) return NotFound(response);
            return Ok(response);
        }

        [HttpDelete("DeleteBy{id}")]
        public async Task<ActionResult<ServiceResponse<GetStudentDto>>> Delete(int id)
        {
            return Ok(await _studentService.Delete(id));
        }

        [HttpGet("GetFullAddressByStudent{id}")]
        public async Task<ActionResult<ServiceResponse<FullAddress>>> GetFullAddressByStudentId(int id)
        {
            return Ok(await _studentService.GetFullAddressByStudentId(id));
        }
    }
}