using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace firstapi.Controllers.AddressControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService<Address, AddAddressDto> _addressService;

        public AddressController(IAddressService<Address, AddAddressDto> addressService)
        {
            _addressService = addressService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<Address>>>> GetAll()
        {
            return Ok(await _addressService.GetAll());
        }

        [HttpGet("GetBy{id}")]
        public async Task<ActionResult<ServiceResponse<Address>>> Get(int id)
        {
            return Ok(await _addressService.GetById(id));
        }

        [HttpPost("AddNew")]
        public async Task<ActionResult<ServiceResponse<List<Address>>>> Add(AddAddressDto address)
        {
            return Ok(await _addressService.AddNew(address));
        }

        [HttpPut("Update")]
        public async Task<ActionResult<ServiceResponse<Address>>> Update(Address updatedAddress)
        {
            var response = await _addressService.Update(updatedAddress);
            if (response.Data is null) return NotFound(response);
            return Ok(response);
        }

        [HttpDelete("DeleteBy{id}")]
        public async Task<ActionResult<ServiceResponse<Address>>> Delete(int id)
        {
            return Ok(await _addressService.Delete(id));
        }

    }
}
