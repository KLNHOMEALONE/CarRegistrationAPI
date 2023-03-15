using CarRegistrationAPI.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CarRegistrationAPI.Repositories
{
    public interface IOwnerRepository : IGenericRepository<Owner>
    {
        Task<Owner> AddAsync(Owner owner);
        Task DeleteAsync(int id);
        Task<ActionResult<VirtualizeResponse<Owner>>> GetAllAsync<T>(QueryParameters queryParameters);
        Task<Owner> GetAsync(int id);
        Task<Owner> GetOwnerAsync(int id);
        Task UpdateAsync(Owner owner);
    }
}
