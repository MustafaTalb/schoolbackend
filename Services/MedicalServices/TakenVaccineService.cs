using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace firstapi.Services.MedicalServices
{
    public class TakenVaccineService : IMedicalService<TakenVaccine, AddTakenVaccine>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public TakenVaccineService(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<TakenVaccine>>> AddNew(AddTakenVaccine newVaccine)
        {
            var serviceResponse = new ServiceResponse<List<TakenVaccine>>();
            var vaccine = _mapper.Map<TakenVaccine>(newVaccine);
            await _context.TakenVaccines.AddAsync(vaccine);
            _context.SaveChanges();
            serviceResponse.Data = await _context.TakenVaccines.ToListAsync();
            serviceResponse.Success = true;
            serviceResponse.Message = "Added the new vaccine to the student and got all taken Vaccines!";
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<TakenVaccine>>> Delete(int id)
        {
            var serviceResponse = new ServiceResponse<List<TakenVaccine>>();
            try
            {
                var vaccine = await _context.TakenVaccines.FirstOrDefaultAsync(c => c.Id == id);
                if (vaccine is null) throw new Exception($"Vaccine with the id {id} is not found.");
                _context.TakenVaccines.Remove(vaccine);
                await _context.SaveChangesAsync();
                serviceResponse.Data = await _context.TakenVaccines.ToListAsync();
                serviceResponse.Message = $"Deleted the vaccine that has the id {id}!";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<TakenVaccine>>> GetAll()
        {
            var serviceResponse = new ServiceResponse<List<TakenVaccine>>();
            serviceResponse.Data = await _context.TakenVaccines.ToListAsync();
            serviceResponse.Success = true;
            serviceResponse.Message = "Got all the vaccines!";
            return serviceResponse;
        }

        public async Task<ServiceResponse<TakenVaccine>> GetById(int id)
        {
            var serviceResponse = new ServiceResponse<TakenVaccine>();
            try
            {
                var vaccine = await _context.TakenVaccines.FirstOrDefaultAsync(v => v.Id == id);
                if (vaccine is null) throw new Exception($"Vaccine with the id {id} is not found.");
                serviceResponse.Data = vaccine;
                serviceResponse.Message = "Found";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<TakenVaccine>> Update(TakenVaccine updatedVaccine)
        {
            var serviceResponse = new ServiceResponse<TakenVaccine>();
            try
            {
                var vaccine = await _context.TakenVaccines.FirstOrDefaultAsync(s => s.Id == updatedVaccine.Id);
                if (vaccine is null) throw new Exception($"Vaccine with the id {updatedVaccine.Id} is not found.");

                vaccine.Notes = updatedVaccine.Notes;
                await _context.SaveChangesAsync();
                serviceResponse.Data = vaccine;
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