using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace firstapi.Services.StudentServices
{
    public interface IStudentService
    {
        Task<ServiceResponse<GetStudentDto>> GetById(int id);
        Task<ServiceResponse<List<GetStudentDto>>> GetAll();
        Task<ServiceResponse<List<GetStudentDto>>> AddNew(AddStudentDto newStudent);
        Task<ServiceResponse<GetStudentDto>> Update(UpdateStudentDto updatedStudent);
        Task<ServiceResponse<List<GetStudentDto>>> Delete(int id);
        Task<ServiceResponse<FullAddress>> GetFullAddressByStudentId(int id);
        Task<ServiceResponse<List<TakenVaccine>>> GetStudentTakenVaccinesByStudentId(int id);
        Task<ServiceResponse<List<StudentIllness>>> GetStudentIllnessesByStudentId(int id);
    }
}