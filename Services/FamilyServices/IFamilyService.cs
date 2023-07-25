using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace firstapi.Services.FamilyServices
{
    public interface IFamilyService<T,T2>
    {
        Task<ServiceResponse<T>> GetById(int id);
        Task<ServiceResponse<List<T>>> GetAll();
        Task<ServiceResponse<T>> AddNew(T2 newItem);
        Task<ServiceResponse<T>> Update(T updatedItem);
        Task<ServiceResponse<T>> Delete(int id);
    }
}