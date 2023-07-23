using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace firstapi.Controllers.AddressControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AreaController : ControllerBase
    {
        private readonly IAddressService<Area, AddAreaDto> _areaService;

        public AreaController(IAddressService<Area, AddAreaDto> areaService)
        {
            _areaService = areaService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<Area>>>> GetAll()
        {
            return Ok(await _areaService.GetAll());
        }

        [HttpGet("GetBy{id}")]
        public async Task<ActionResult<ServiceResponse<Area>>> Get(int id)
        {
            return Ok(await _areaService.GetById(id));
        }

        [HttpPost("AddNew")]
        public async Task<ActionResult<ServiceResponse<List<Area>>>> Add(AddAreaDto area)
        {
            return Ok(await _areaService.AddNew(area));
        }

        [HttpPut("Update")]
        public async Task<ActionResult<ServiceResponse<Area>>> Update(Area updatedArea)
        {
            var response = await _areaService.Update(updatedArea);
            if (response.Data is null) return NotFound(response);
            return Ok(response);
        }

        [HttpDelete("DeleteBy{id}")]
        public async Task<ActionResult<ServiceResponse<Area>>> Delete(int id)
        {
            return Ok(await _areaService.Delete(id));
        }

    }
}