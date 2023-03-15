using CarRegistrationAPI.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CarRegistrationAPI.Repositories
{
    public interface ICertificateRepository : IGenericRepository<Certificate>
    {
        Task<Certificate> AddAsync(Certificate cert);
        Task DeleteAsync(int id);
        Task<ActionResult<VirtualizeResponse<Certificate>>> GetAllAsync<T>(QueryParameters queryParameters);
        Task<Certificate> GetAsync(int id);
        Task<Certificate> GetCertificateAsync(int id);
        Task UpdateAsync(Certificate certificate);
    }
}
