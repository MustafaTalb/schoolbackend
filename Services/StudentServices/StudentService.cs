using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace firstapi.Services.StudentServices
{
    public class StudentService : IStudentService
    {

        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public StudentService(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<List<GetStudentDto>>> AddNew(AddStudentDto newStudent)
        {
            var serviceResponse = new ServiceResponse<List<GetStudentDto>>();
            var student = _mapper.Map<Student>(newStudent);
            await _context.Students.AddAsync(student);
            _context.SaveChanges();
            serviceResponse.Data = await _context.Students.Select(s => _mapper.Map<GetStudentDto>(s)).ToListAsync();
            serviceResponse.Success = true;
            serviceResponse.Message = "Added the new student and got all students!";
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetStudentDto>>> Delete(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetStudentDto>>();
            try
            {
                var student = await _context.Students.FirstOrDefaultAsync(s => s.Id == id);
                if (student is null) throw new Exception($"Student with the id {id} is not found.");
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
                serviceResponse.Data = await _context.Students.Select(s => _mapper.Map<GetStudentDto>(s)).ToListAsync();
                serviceResponse.Message = $"Deleted the student that has the id {id}!";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;

        }

        public async Task<ServiceResponse<List<GetStudentDto>>> GetAll()
        {
            var serviceResponse = new ServiceResponse<List<GetStudentDto>>();
            var dbStudents = await _context.Students.ToListAsync();
            serviceResponse.Message = $"Done and found {dbStudents.Count} students!";
            serviceResponse.Data = dbStudents.Select(s => _mapper.Map<GetStudentDto>(s)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetStudentDto>> GetById(int id)
        {
            var serviceResponse = new ServiceResponse<GetStudentDto>();
            try
            {
                var dbStudent = await _context.Students.FirstOrDefaultAsync(c => c.Id == id);
                if (dbStudent is null) throw new Exception($"Student with the id {id} is not found.");
                serviceResponse.Data = _mapper.Map<GetStudentDto>(dbStudent);
                serviceResponse.Message = "Found";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetStudentDto>> Update(UpdateStudentDto updatedStudent)
        {
            var serviceResponse = new ServiceResponse<GetStudentDto>();
            try
            {
                var student = await _context.Students.FirstOrDefaultAsync(s => s.Id == updatedStudent.Id);
                if (student is null) throw new Exception($"Student with the id {updatedStudent.Id} is not found.");

                student.FirstName = updatedStudent.FirstName;
                student.IsMale = updatedStudent.IsMale;
                student.PlaceOfBirth = updatedStudent.PlaceOfBirth;
                student.DateOfBirth = updatedStudent.DateOfBirth;
                student.Religion = updatedStudent.Religion;
                student.JoinDate = updatedStudent.JoinDate;
                student.LeaveDate = updatedStudent.LeaveDate;
                student.Landline = updatedStudent.Landline;
                student.IsActive = updatedStudent.IsActive;
                student.Height = updatedStudent.Height;
                student.Weight = updatedStudent.Weight;
                student.AddressId = updatedStudent.AddressId;

                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetStudentDto>(student);
                serviceResponse.Message = "Done Updating";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<FullAddress>> GetFullAddressByStudentId(int studentId)
        {
            var serviceResponse = new ServiceResponse<FullAddress>();
            var student = await _context.Students.FirstOrDefaultAsync(s => s.Id == studentId);
            if (student != null)
            {
                var address = await _context.Addresses.FirstOrDefaultAsync(a => a.Id == student!.AddressId);
                var street = await _context.Streets.FirstOrDefaultAsync(s => s.Id == address!.StreetId);
                var area = await _context.Areas.FirstOrDefaultAsync(a => a.Id == street!.AreaId);
                var city = await _context.Cities.FirstOrDefaultAsync(c => c.Id == area!.CityId);
                var fullAddress = new FullAddress { City = city!, Area = area!, Street = street!, Address = address! };

                serviceResponse.Data = fullAddress;
                serviceResponse.Success = true;
                serviceResponse.Message = "done!";
                return serviceResponse;
            }
            else
            {
                serviceResponse.Success = false; serviceResponse.Message = "Student Not Found!";
                return serviceResponse;
            }
        }
        public async Task<ServiceResponse<List<StudentIllness>>> GetStudentIllnessesByStudentId(int studentId)
        {
            var serviceResponse = new ServiceResponse<List<StudentIllness>>();
            List<StudentIllness> illnessesList = await _context.StudentIllnesses.ToListAsync();
            List<StudentIllness> list = new List<StudentIllness>();
            foreach (StudentIllness i in illnessesList)
            {
                if (i.StudentId == studentId) { list.Add(i); }
            }
            serviceResponse.Data = list;
            serviceResponse.Success = true;
            serviceResponse.Message = "Got all student illnesses!";
            return serviceResponse;
        }
        public async Task<ServiceResponse<List<TakenVaccine>>> GetStudentTakenVaccinesByStudentId(int studentId)
        {
            var serviceResponse = new ServiceResponse<List<TakenVaccine>>();
            List<TakenVaccine> illnessesList = await _context.TakenVaccines.ToListAsync();
            List<TakenVaccine> list = new List<TakenVaccine>();
            foreach (TakenVaccine v in illnessesList)
            {
                if (v.StudentId == studentId) { list.Add(v); }
            }
            serviceResponse.Data = list;
            serviceResponse.Success = true;
            serviceResponse.Message = "Got all student taken vaccines!";
            return serviceResponse;
        }
    }
}