using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace firstapi.Services.AddressServices
{
    public class AreaService : IAddressService<Area, AddAreaDto>

    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public AreaService(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<Area>>> AddNew(AddAreaDto newArea)
        {
            var serviceResponse = new ServiceResponse<List<Area>>();
            var area = _mapper.Map<Area>(newArea);
            await _context.Areas.AddAsync(area);
            _context.SaveChanges();
            serviceResponse.Data = await _context.Areas.ToListAsync();
            serviceResponse.Success = true;
            serviceResponse.Message = "Added the new area and got all areas!";
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Area>>> Delete(int id)
        {
            var serviceResponse = new ServiceResponse<List<Area>>();
            try
            {
                var area = await _context.Areas.FirstOrDefaultAsync(a => a.Id == id);
                if (area is null) throw new Exception($"Area with the id {id} is not found.");
                _context.Areas.Remove(area);
                await _context.SaveChangesAsync();
                serviceResponse.Data = await _context.Areas.ToListAsync();
                serviceResponse.Message = $"Deleted the area that has the id {id}!";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Area>>> GetAll()
        {
            var serviceResponse = new ServiceResponse<List<Area>>();
            serviceResponse.Data = await _context.Areas.ToListAsync();
            serviceResponse.Success = true;
            serviceResponse.Message = "Got all areas!";
            return serviceResponse;
        }

        public async Task<ServiceResponse<Area>> GetById(int id)
        {
            var serviceResponse = new ServiceResponse<Area>();
            try
            {
                var dbArea = await _context.Areas.FirstOrDefaultAsync(a => a.Id == id);
                if (dbArea is null) throw new Exception($"area with the id {id} is not found.");
                serviceResponse.Data = dbArea;
                serviceResponse.Message = "Found";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<Area>> Update(Area updatedArea)
        {
            var serviceResponse = new ServiceResponse<Area>();
            try
            {
                var area = await _context.Areas.FirstOrDefaultAsync(s => s.Id == updatedArea.Id);
                if (area is null) throw new Exception($"area with the id {updatedArea.Id} is not found.");

                area.Name = updatedArea.Name;
                await _context.SaveChangesAsync();
                serviceResponse.Data = area;
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