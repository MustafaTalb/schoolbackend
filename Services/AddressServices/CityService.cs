using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace firstapi.Services.AddressServices
{
    public class CityService : IAddressService<City, AddCityDto>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CityService(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<List<City>>> AddNew(AddCityDto newCity)
        {
            var serviceResponse = new ServiceResponse<List<City>>();
            var city = _mapper.Map<City>(newCity);
            await _context.Cities.AddAsync(city);
            _context.SaveChanges();
            serviceResponse.Data = await _context.Cities.ToListAsync();
            serviceResponse.Success = true;
            serviceResponse.Message = "Added the new City and got all Cities!";
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<City>>> Delete(int id)
        {
            var serviceResponse = new ServiceResponse<List<City>>();
            try
            {
                var city = await _context.Cities.FirstOrDefaultAsync(c => c.Id == id);
                if (city is null) throw new Exception($"City with the id {id} is not found.");
                _context.Cities.Remove(city);
                await _context.SaveChangesAsync();
                serviceResponse.Data = await _context.Cities.ToListAsync();
                serviceResponse.Message = $"Deleted the city that has the id {id}!";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<City>>> GetAll()
        {
            var serviceResponse = new ServiceResponse<List<City>>();
            serviceResponse.Data = await _context.Cities.ToListAsync();
            serviceResponse.Success = true;
            serviceResponse.Message = "Got all Cities!";
            return serviceResponse;
        }

        public async Task<ServiceResponse<City>> GetById(int id)
        {
            var serviceResponse = new ServiceResponse<City>();
            try
            {
                var dbCity = await _context.Cities.FirstOrDefaultAsync(c => c.Id == id);
                if (dbCity is null) throw new Exception($"City with the id {id} is not found.");
                serviceResponse.Data = dbCity;
                serviceResponse.Message = "Found";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<City>> Update(City updatedCity)
        {
            var serviceResponse = new ServiceResponse<City>();
            try
            {
                var city = await _context.Cities.FirstOrDefaultAsync(s => s.Id == updatedCity.Id);
                if (city is null) throw new Exception($"City with the id {updatedCity.Id} is not found.");

                city.Name = updatedCity.Name;
                await _context.SaveChangesAsync();
                serviceResponse.Data = city;
                serviceResponse.Message = "Done Updating";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
    }
}