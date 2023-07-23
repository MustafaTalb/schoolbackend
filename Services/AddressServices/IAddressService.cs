using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace firstapi.Services.AddressServices
{
    public interface IAddressService<T,AddT>
    {
        Task<ServiceResponse<T>> GetById(int id);
        Task<ServiceResponse<List<T>>> GetAll();
        Task<ServiceResponse<List<T>>> AddNew(AddT newItem);
        Task<ServiceResponse<T>> Update(T updatedItem);
        Task<ServiceResponse<List<T>>> Delete(int id);
    }
}