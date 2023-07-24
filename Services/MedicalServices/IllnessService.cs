using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace firstapi.Services.MedicalServices
{
    public class IllnessService : IMedicalService<Illness, AddIllnessDto>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public IllnessService(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<List<Illness>>> AddNew(AddIllnessDto newIllness)
        {
            var serviceResponse = new ServiceResponse<List<Illness>>();
            var illness = _mapper.Map<Illness>(newIllness);
            await _context.Illnesses.AddAsync(illness);
            _context.SaveChanges();
            serviceResponse.Data = await _context.Illnesses.ToListAsync();
            serviceResponse.Success = true;
            serviceResponse.Message = "Added the new illness and got all illnesses!";
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Illness>>> Delete(int id)
        {
            var serviceResponse = new ServiceResponse<List<Illness>>();
            try
            {
                var illness = await _context.Illnesses.FirstOrDefaultAsync(i => i.Id == id);
                if (illness is null) throw new Exception($"Illness with the id {id} is not found.");
                _context.Illnesses.Remove(illness);
                await _context.SaveChangesAsync();
                serviceResponse.Data = await _context.Illnesses.ToListAsync();
                serviceResponse.Message = $"Deleted the illness that has the id {id}!";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Illness>>> GetAll()
        {
            var serviceResponse = new ServiceResponse<List<Illness>>();
            serviceResponse.Data = await _context.Illnesses.ToListAsync();
            serviceResponse.Success = true;
            serviceResponse.Message = "Got all illnesses!";
            return serviceResponse;
        }

        public async Task<ServiceResponse<Illness>> GetById(int id)
        {
            var serviceResponse = new ServiceResponse<Illness>();
            try
            {
                var illness = await _context.Illnesses.FirstOrDefaultAsync(c => c.Id == id);
                if (illness is null) throw new Exception($"Illness with the id {id} is not found.");
                serviceResponse.Data = illness;
                serviceResponse.Message = "Found";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<Illness>> Update(Illness updatedIllness)
        {
            var serviceResponse = new ServiceResponse<Illness>();
            try
            {
                var illness = await _context.Illnesses.FirstOrDefaultAsync(s => s.Id == updatedIllness.Id);
                if (illness is null) throw new Exception($"Illness with the id {updatedIllness.Id} is not found.");

                illness.Name = updatedIllness.Name;
                await _context.SaveChangesAsync();
                serviceResponse.Data = illness;
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
