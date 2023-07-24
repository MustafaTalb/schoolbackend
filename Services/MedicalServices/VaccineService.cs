using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace firstapi.Services.MedicalServices
{
    public class VaccineService : IMedicalService<Vaccine, AddVaccineDto>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public VaccineService(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<List<Vaccine>>> AddNew(AddVaccineDto newVaccine)
        {
            var serviceResponse = new ServiceResponse<List<Vaccine>>();
            var vaccine = _mapper.Map<Vaccine>(newVaccine);
            await _context.Vaccines.AddAsync(vaccine);
            _context.SaveChanges();
            serviceResponse.Data = await _context.Vaccines.ToListAsync();
            serviceResponse.Success = true;
            serviceResponse.Message = "Added the new vaccine and got all vaccines!";
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Vaccine>>> Delete(int id)
        {
            var serviceResponse = new ServiceResponse<List<Vaccine>>();
            try
            {
                var vaccine = await _context.Vaccines.FirstOrDefaultAsync(c => c.Id == id);
                if (vaccine is null) throw new Exception($"Vaccine with the id {id} is not found.");
                _context.Vaccines.Remove(vaccine);
                await _context.SaveChangesAsync();
                serviceResponse.Data = await _context.Vaccines.ToListAsync();
                serviceResponse.Message = $"Deleted the vaccine that has the id {id}!";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Vaccine>>> GetAll()
        {
            var serviceResponse = new ServiceResponse<List<Vaccine>>();
            serviceResponse.Data = await _context.Vaccines.ToListAsync();
            serviceResponse.Success = true;
            serviceResponse.Message = "Got all the vaccines!";
            return serviceResponse;
        }

        public async Task<ServiceResponse<Vaccine>> GetById(int id)
        {
            var serviceResponse = new ServiceResponse<Vaccine>();
            try
            {
                var vaccine = await _context.Vaccines.FirstOrDefaultAsync(v => v.Id == id);
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

        public async Task<ServiceResponse<Vaccine>> Update(Vaccine updatedVaccine)
        {
            var serviceResponse = new ServiceResponse<Vaccine>();
            try
            {
                var vaccine = await _context.Vaccines.FirstOrDefaultAsync(s => s.Id == updatedVaccine.Id);
                if (vaccine is null) throw new Exception($"Vaccine with the id {updatedVaccine.Id} is not found.");

                vaccine.Name = updatedVaccine.Name;
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