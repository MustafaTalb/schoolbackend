using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace firstapi.Services.MedicalServices
{
    public class StudentIllnessService : IMedicalService<StudentIllness, AddStudentIllness>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public StudentIllnessService(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<List<StudentIllness>>> AddNew(AddStudentIllness newIllness)
        {
            var serviceResponse = new ServiceResponse<List<StudentIllness>>();
            var illness = _mapper.Map<StudentIllness>(newIllness);
            await _context.StudentIllnesses.AddAsync(illness);
            _context.SaveChanges();
            serviceResponse.Data = await _context.StudentIllnesses.ToListAsync();
            serviceResponse.Success = true;
            serviceResponse.Message = "Added the new illness and got all student illnesses!";
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<StudentIllness>>> Delete(int id)
        {
            var serviceResponse = new ServiceResponse<List<StudentIllness>>();
            try
            {
                var illness = await _context.StudentIllnesses.FirstOrDefaultAsync(i => i.Id == id);
                if (illness is null) throw new Exception($"Illness with the id {id} is not found.");
                _context.StudentIllnesses.Remove(illness);
                await _context.SaveChangesAsync();
                serviceResponse.Data = await _context.StudentIllnesses.ToListAsync();
                serviceResponse.Message = $"Deleted the illness that has the id {id}!";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<StudentIllness>>> GetAll()
        {
            var serviceResponse = new ServiceResponse<List<StudentIllness>>();
            serviceResponse.Data = await _context.StudentIllnesses.ToListAsync();
            serviceResponse.Success = true;
            serviceResponse.Message = "Got all illnesses!";
            return serviceResponse;
        }

        public async Task<ServiceResponse<StudentIllness>> GetById(int id)
        {
            var serviceResponse = new ServiceResponse<StudentIllness>();
            try
            {
                var illness = await _context.StudentIllnesses.FirstOrDefaultAsync(c => c.Id == id);
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

        public async Task<ServiceResponse<StudentIllness>> Update(StudentIllness updatedIllness)
        {
            var serviceResponse = new ServiceResponse<StudentIllness>();
            try
            {
                var illness = await _context.StudentIllnesses.FirstOrDefaultAsync(s => s.Id == updatedIllness.Id);
                if (illness is null) throw new Exception($"Illness with the id {updatedIllness.Id} is not found.");

                illness.IsCured = updatedIllness.IsCured;
                illness.Notes = updatedIllness.Notes;
                if (updatedIllness.DateOfTreatment != null) illness.DateOfTreatment = updatedIllness.DateOfTreatment;
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