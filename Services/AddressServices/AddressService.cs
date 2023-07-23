using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace firstapi.Services.AddressServices
{
    public class AddressService : IAddressService<Address, AddAddressDto>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public AddressService(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<Address>>> AddNew(AddAddressDto newAddress)
        {
            var serviceResponse = new ServiceResponse<List<Address>>();
            var address = _mapper.Map<Address>(newAddress);
            await _context.Addresses.AddAsync(address);
            _context.SaveChanges();
            serviceResponse.Data = await _context.Addresses.ToListAsync();
            serviceResponse.Success = true;
            serviceResponse.Message = "Added the new address and got all Addresses!";
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Address>>> Delete(int id)
        {
            var serviceResponse = new ServiceResponse<List<Address>>();
            try
            {
                var address = await _context.Addresses.FirstOrDefaultAsync(c => c.Id == id);
                if (address is null) throw new Exception($"Address with the id {id} is not found.");
                _context.Addresses.Remove(address);
                await _context.SaveChangesAsync();
                serviceResponse.Data = await _context.Addresses.ToListAsync();
                serviceResponse.Message = $"Deleted the Address that has the id {id}!";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Address>>> GetAll()
        {
            var serviceResponse = new ServiceResponse<List<Address>>();
            serviceResponse.Data = await _context.Addresses.ToListAsync();
            serviceResponse.Success = true;
            serviceResponse.Message = "Got all Addresses!";
            return serviceResponse;
        }

        public async Task<ServiceResponse<Address>> GetById(int id)
        {
            var serviceResponse = new ServiceResponse<Address>();
            try
            {
                var dbAddress = await _context.Addresses.FirstOrDefaultAsync(a => a.Id == id);
                if (dbAddress is null) throw new Exception($"Address with the id {id} is not found.");
                serviceResponse.Data = dbAddress;
                serviceResponse.Message = "Found";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<Address>> Update(Address updatedAddress)
        {
            var serviceResponse = new ServiceResponse<Address>();
            try
            {
                var address = await _context.Addresses.FirstOrDefaultAsync(s => s.Id == updatedAddress.Id);
                if (address is null) throw new Exception($"City with the id {updatedAddress.Id} is not found.");

                address.Name = updatedAddress.Name;
                if (updatedAddress.Details != null) address.Details = updatedAddress.Details;
                await _context.SaveChangesAsync();
                serviceResponse.Data = address;
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