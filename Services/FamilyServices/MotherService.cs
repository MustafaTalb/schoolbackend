using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace firstapi.Services.FamilyServices
{
    public class MotherService : IFamilyService<Mother, AddMotherDto>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public MotherService(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<Mother>> AddNew(AddMotherDto newItem)
        {
            var serviceResponse = new ServiceResponse<Mother>();
            var mother = _mapper.Map<Mother>(newItem);
            await _context.Mothers.AddAsync(mother);
            _context.SaveChanges();
            serviceResponse.Data = mother;
            serviceResponse.Success = true;
            serviceResponse.Message = "Added the new mother!";
            return serviceResponse;
        }

        public async Task<ServiceResponse<Mother>> Delete(int id)
        {
            var serviceResponse = new ServiceResponse<Mother>();
            try
            {
                var mother = await _context.Mothers.FirstOrDefaultAsync(m => m.Id == id);
                if (mother is null) throw new Exception($"Father with the id {id} is not found.");
                _context.Mothers.Remove(mother);
                await _context.SaveChangesAsync();
                serviceResponse.Data = mother;
                serviceResponse.Message = $"Deleted the father that has the id {id}!";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Mother>>> GetAll()
        {
            var serviceResponse = new ServiceResponse<List<Mother>>();
            serviceResponse.Data = await _context.Mothers.ToListAsync();
            serviceResponse.Success = true;
            serviceResponse.Message = "Got all mothers!";
            return serviceResponse;
        }

        public async Task<ServiceResponse<Mother>> GetById(int id)
        {
            var serviceResponse = new ServiceResponse<Mother>();
            try
            {
                var mother = await _context.Mothers.FirstOrDefaultAsync(m => m.Id == id);
                if (mother is null) throw new Exception($"Mother with the id {id} is not found.");
                serviceResponse.Data = mother;
                serviceResponse.Message = "Found";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<Mother>> Update(Mother updatedItem)
        {
            var serviceResponse = new ServiceResponse<Mother>();
            try
            {
                var mother = await _context.Mothers.FirstOrDefaultAsync(s => s.Id == updatedItem.Id);
                if (mother is null) throw new Exception($"Mother with the id {updatedItem.Id} is not found.");

                mother.FirstName = updatedItem.FirstName;
                mother.LastName = updatedItem.LastName;
                mother.FatherName = updatedItem.FatherName;
                mother.MotherName = updatedItem.MotherName;
                mother.LivesWithHusband = updatedItem.LivesWithHusband;
                mother.Career = updatedItem.Career;
                mother.DateOfBirth = updatedItem.DateOfBirth;
                mother.PlaceOfBirth = updatedItem.PlaceOfBirth;
                mother.Religion = updatedItem.Religion;
                mother.TieNumber = updatedItem.TieNumber;
                mother.TiePlace = updatedItem.TiePlace;
                mother.EducationStatus = updatedItem.EducationStatus;
                mother.PhoneNumber = updatedItem.PhoneNumber;

                await _context.SaveChangesAsync();
                serviceResponse.Data = mother;
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