using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace firstapi.Services.AddressServices
{
    public class StreetService : IAddressService<Street, AddStreetDto>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public StreetService(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<List<Street>>> AddNew(AddStreetDto newStreet)
        {
            var serviceResponse = new ServiceResponse<List<Street>>();
            var street = _mapper.Map<Street>(newStreet);
            await _context.Streets.AddAsync(street);
            _context.SaveChanges();
            serviceResponse.Data = await _context.Streets.ToListAsync();
            serviceResponse.Success = true;
            serviceResponse.Message = "Added the new street and got all streets!";
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Street>>> Delete(int id)
        {
            var serviceResponse = new ServiceResponse<List<Street>>();
            try
            {
                var street = await _context.Streets.FirstOrDefaultAsync(s => s.Id == id);
                if (street is null) throw new Exception($"Street with the id {id} is not found.");
                _context.Streets.Remove(street);
                await _context.SaveChangesAsync();
                serviceResponse.Data = await _context.Streets.ToListAsync();
                serviceResponse.Message = $"Deleted the street that has the id {id}!";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Street>>> GetAll()
        {
            var serviceResponse = new ServiceResponse<List<Street>>();
            serviceResponse.Data = await _context.Streets.ToListAsync();
            serviceResponse.Success = true;
            serviceResponse.Message = "Got all streets!";
            return serviceResponse;
        }

        public async Task<ServiceResponse<Street>> GetById(int id)
        {
            var serviceResponse = new ServiceResponse<Street>();
            try
            {
                var dbStreet = await _context.Streets.FirstOrDefaultAsync(s => s.Id == id);
                if (dbStreet is null) throw new Exception($"Street with the id {id} is not found.");
                serviceResponse.Data = dbStreet;
                serviceResponse.Message = "Found";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<Street>> Update(Street updatedStreet)
        {
            var serviceResponse = new ServiceResponse<Street>();
            try
            {
                var street = await _context.Streets.FirstOrDefaultAsync(s => s.Id == updatedStreet.Id);
                if (street is null) throw new Exception($"Street with the id {updatedStreet.Id} is not found.");

                street.Name = updatedStreet.Name;
                await _context.SaveChangesAsync();
                serviceResponse.Data = street;
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