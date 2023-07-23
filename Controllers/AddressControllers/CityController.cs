using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace firstapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CityController : ControllerBase
    {
        private readonly IAddressService<City,AddCityDto> _cityService;

        public CityController(IAddressService<City,AddCityDto> cityService)
        {
            _cityService = cityService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<City>>>> GetAll()
        {
            return Ok(await _cityService.GetAll());
        }

        [HttpGet("GetBy{id}")]
        public async Task<ActionResult<ServiceResponse<City>>> Get(int id)
        {
            return Ok(await _cityService.GetById(id));
        }

        [HttpPost("AddNew")]
        public async Task<ActionResult<ServiceResponse<List<City>>>> Add(AddCityDto city)
        {
            return Ok(await _cityService.AddNew(city));
        }

        [HttpPut("Update")]
        public async Task<ActionResult<ServiceResponse<City>>> Update(City updateCity)
        {
            var response = await _cityService.Update(updateCity);
            if (response.Data is null) return NotFound(response);
            return Ok(response);
        }

        [HttpDelete("DeleteBy{id}")]
        public async Task<ActionResult<ServiceResponse<City>>> Delete(int id)
        {
            return Ok(await _cityService.Delete(id));
        }
    }
}