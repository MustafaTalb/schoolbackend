using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace firstapi.Services.FamilyServices
{
    public class FamilyService : IFamilyService<Family, AddFamilyDto>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public FamilyService(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<Family>> AddNew(AddFamilyDto newItem)
        {
            var serviceResponse = new ServiceResponse<Family>();
            var family = _mapper.Map<Family>(newItem);
            await _context.families.AddAsync(family);
            _context.SaveChanges();
            serviceResponse.Data = family;
            serviceResponse.Success = true;
            serviceResponse.Message = "Added the new family!";
            return serviceResponse;
        }

        public async Task<ServiceResponse<Family>> Delete(int id)
        {
            var serviceResponse = new ServiceResponse<Family>();
            try
            {
                var family = await _context.families.FirstOrDefaultAsync(f => f.Id == id);
                if (family is null) throw new Exception($"Family with the id {id} is not found.");
                _context.families.Remove(family);
                await _context.SaveChangesAsync();
                serviceResponse.Data = family;
                serviceResponse.Message = $"Deleted the family that has the id {id}!";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Family>>> GetAll()
        {
            var serviceResponse = new ServiceResponse<List<Family>>();
            serviceResponse.Data = await _context.families.ToListAsync();
            serviceResponse.Success = true;
            serviceResponse.Message = "Got all families!";
            return serviceResponse;
        }

        public async Task<ServiceResponse<Family>> GetById(int id)
        {
            var serviceResponse = new ServiceResponse<Family>();
            try
            {
                var father = await _context.families.FirstOrDefaultAsync(f => f.Id == id);
                if (father is null) throw new Exception($"Family with the id {id} is not found.");
                serviceResponse.Data = father;
                serviceResponse.Message = "Found";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<Family>> Update(Family updatedItem)
        {
            var serviceResponse = new ServiceResponse<Family>();
            try
            {
                var family = await _context.families.FirstOrDefaultAsync(f => f.Id == updatedItem.Id);
                if (family is null) throw new Exception($"Family with the id {updatedItem.Id} is not found.");

                family.UserName = updatedItem.UserName;
                family.Password = updatedItem.Password;
                family.FatherId = updatedItem.FatherId;
                family.MotherId = updatedItem.MotherId;

                await _context.SaveChangesAsync();
                serviceResponse.Data = family;
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