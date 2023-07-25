using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace firstapi.Services.FamilyServices
{
    public class FatherService : IFamilyService<Father, AddFatherDto>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public FatherService(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<Father>> AddNew(AddFatherDto newItem)
        {
            var serviceResponse = new ServiceResponse<Father>();
            var father = _mapper.Map<Father>(newItem);
            await _context.Fathers.AddAsync(father);
            _context.SaveChanges();
            serviceResponse.Data = father;
            serviceResponse.Success = true;
            serviceResponse.Message = "Added the new father!";
            return serviceResponse;
        }

        public async Task<ServiceResponse<Father>> Delete(int id)
        {
            var serviceResponse = new ServiceResponse<Father>();
            try
            {
                var father = await _context.Fathers.FirstOrDefaultAsync(f => f.Id == id);
                if (father is null) throw new Exception($"Father with the id {id} is not found.");
                _context.Fathers.Remove(father);
                await _context.SaveChangesAsync();
                serviceResponse.Data = father;
                serviceResponse.Message = $"Deleted the father that has the id {id}!";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Father>>> GetAll()
        {
            var serviceResponse = new ServiceResponse<List<Father>>();
            serviceResponse.Data = await _context.Fathers.ToListAsync();
            serviceResponse.Success = true;
            serviceResponse.Message = "Got all fathers!";
            return serviceResponse;
        }

        public async Task<ServiceResponse<Father>> GetById(int id)
        {
            var serviceResponse = new ServiceResponse<Father>();
            try
            {
                var father = await _context.Fathers.FirstOrDefaultAsync(f => f.Id == id);
                if (father is null) throw new Exception($"Father with the id {id} is not found.");
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

        public async Task<ServiceResponse<Father>> Update(Father updatedItem)
        {
            var serviceResponse = new ServiceResponse<Father>();
            try
            {
                var father = await _context.Fathers.FirstOrDefaultAsync(s => s.Id == updatedItem.Id);
                if (father is null) throw new Exception($"Father with the id {updatedItem.Id} is not found.");

                father.FirstName = updatedItem.FirstName;
                father.LastName = updatedItem.LastName;
                father.FatherName = updatedItem.FatherName;
                father.MotherName = updatedItem.MotherName;
                father.Career = updatedItem.Career;
                father.PlaceOfResidence = updatedItem.PlaceOfResidence;
                father.DateOfBirth = updatedItem.DateOfBirth;
                father.PlaceOfBirth = updatedItem.PlaceOfBirth;
                father.Religion = updatedItem.Religion;
                father.TieNumber = updatedItem.TieNumber;
                father.TiePlace = updatedItem.TiePlace;
                father.EducationStatus=updatedItem.EducationStatus;
                father.PhoneNumber = updatedItem.PhoneNumber;

                await _context.SaveChangesAsync();
                serviceResponse.Data = father;
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