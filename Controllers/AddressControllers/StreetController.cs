using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace firstapi.Controllers.AddressControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StreetController : ControllerBase
    {
        private readonly IAddressService<Street, AddStreetDto> _streetService;

        public StreetController(IAddressService<Street, AddStreetDto> streetService)
        {
            _streetService = streetService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<Street>>>> GetAll()
        {
            return Ok(await _streetService.GetAll());
        }

        [HttpGet("GetBy{id}")]
        public async Task<ActionResult<ServiceResponse<Street>>> Get(int id)
        {
            return Ok(await _streetService.GetById(id));
        }

        [HttpPost("AddNew")]
        public async Task<ActionResult<ServiceResponse<List<Street>>>> Add(AddStreetDto street)
        {
            return Ok(await _streetService.AddNew(street));
        }

        [HttpPut("Update")]
        public async Task<ActionResult<ServiceResponse<Street>>> Update(Street updatedStreet)
        {
            var response = await _streetService.Update(updatedStreet);
            if (response.Data is null) return NotFound(response);
            return Ok(response);
        }

        [HttpDelete("DeleteBy{id}")]
        public async Task<ActionResult<ServiceResponse<Street>>> Delete(int id)
        {
            return Ok(await _streetService.Delete(id));
        }

    }
}
