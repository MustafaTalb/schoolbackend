using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace firstapi.Controllers.FamilyControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FatherController : ControllerBase
    {
        private readonly IFamilyService<Father, AddFatherDto> _fatherService;
        public FatherController(IFamilyService<Father, AddFatherDto> fatherService)
        {
            _fatherService = fatherService;
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<Father>>>> GetAll()
        {
            return Ok(await _fatherService.GetAll());
        }

        [HttpGet("GetBy{id}")]
        public async Task<ActionResult<ServiceResponse<Father>>> GetById(int id)
        {
            return Ok(await _fatherService.GetById(id));
        }

        [HttpPost("AddNew")]
        public async Task<ActionResult<ServiceResponse<Father>>> Add(AddFatherDto father)
        {
            return Ok(await _fatherService.AddNew(father));
        }

        [HttpPut("Update")]
        public async Task<ActionResult<ServiceResponse<Father>>> Update(Father updatedFather)
        {
            var response = await _fatherService.Update(updatedFather);
            if (response.Data is null) return NotFound(response);
            return Ok(response);
        }

        [HttpDelete("DeleteBy{id}")]
        public async Task<ActionResult<ServiceResponse<Father>>> Delete(int id)
        {
            return Ok(await _fatherService.Delete(id));
        }
    }
}