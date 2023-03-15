using CarRegistrationAPI.Context;
using Dapper;

namespace CarRegistrationAPI.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly DapperContext _context;

        public GenericRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<T> AddAsync(T entity, string sqlQuery, DynamicParameters dp)
        {
            await ExecuteQuery(sqlQuery, dp);
            return entity;
        }

        public async Task DeleteAsync(int id, string query)
        {
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { Id = id });
            }
        }

        public async Task<bool> Exists(int id, string query)
        {
            var entity = await GetAsync(id, query);
            return entity != null;
        }

        public async Task<List<T>> GetAllAsync(string sqlQuery)
        {
            using (var connection = _context.CreateConnection()) 
            {
                var items = await connection.QueryAsync<T>(sqlQuery);
                return items.ToList();
            }
        }

        public async Task<T> GetAsync(int? id, string query)
        {
            if (id == null)
            {
                return null;
            }
            using (var connection = _context.CreateConnection())
            {
                var item = await connection.QuerySingleAsync<T>(query, new { Id = id });
                return item;
            }
        }


        public async Task<VirtualizeResponse<TResult>> GetAllAsync<TResult>(QueryParameters queryParam, string query)
            where TResult : class
        {
            //https://www.learmoreseekmore.com/2021/08/demo-on-dotnet5-web-api-pagination-using-dapper-orm.html
            //https://www.youtube.com/watch?v=PWHvt34Vr1A
            int currentPageNumber = queryParam.StartIndex; 
            int pageSize = queryParam.PageSize;

            int maxPagSize = 50;
            pageSize = (pageSize > 0 && pageSize <= maxPagSize) ? pageSize : maxPagSize;

            int skip = (currentPageNumber - 1) * pageSize;
            int take = pageSize;



            using (var connection = _context.CreateConnection()) 
            {
                var reader = connection.QueryMultiple(query, new { Skip = skip, Take = take });

                int count = reader.Read<int>().FirstOrDefault();
                List<TResult> items = reader.Read<TResult>().ToList();
                return new VirtualizeResponse<TResult> { Items = items, TotalSize = count };
            }


        }

        public async Task UpdateAsync(T entity, string sqlQuery, DynamicParameters dp)
        {
            await ExecuteQuery(sqlQuery, dp);
        }

        private async Task ExecuteQuery(string sqlQuery, DynamicParameters dp)
        {
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(sqlQuery, dp);
            }
        }

    }
}
