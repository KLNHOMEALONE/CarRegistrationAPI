using Dapper;

namespace CarRegistrationAPI.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> AddAsync(T entity, string sqlQuery, DynamicParameters dp);
        Task DeleteAsync(int id, string query);
        Task<bool> Exists(int id, string query);
        Task<List<T>> GetAllAsync(string sqlQuery);
        Task<VirtualizeResponse<TResult>> GetAllAsync<TResult>(QueryParameters queryParam, string query) where TResult : class;
        Task<T> GetAsync(int? id, string query);
        Task UpdateAsync(T entity, string sqlQuery, DynamicParameters dp);
    }
}
