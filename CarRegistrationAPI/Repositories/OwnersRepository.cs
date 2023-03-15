using CarRegistrationAPI.Context;
using CarRegistrationAPI.Entities;
using Dapper;
using Microsoft.AspNetCore.Mvc;

namespace CarRegistrationAPI.Repositories
{
    public class OwnersRepository : GenericRepository<Owner>, IOwnerRepository
    {
        public OwnersRepository(DapperContext context) : base(context)
        {

        }

        public async Task<Owner> AddAsync(Owner owner)
        {
            string query = @"INSERT INTO public.""Owners"" (""Firstname"", ""Lastname"", ""Middlename"", ""Birthdate"", ""Address"") VALUES(@Firstname, @Lastname, @Middlename, @Birthdate, @Address)";
            var dp = new DynamicParameters();
            dp.Add("Firstname", owner.Firstname);
            dp.Add("Lastname", owner.Lastname);
            dp.Add("Middlename", owner.Middlename);
            dp.Add("Birthdate", owner.Birthdate);
            dp.Add("Address", owner.Address);
            return await base.AddAsync(owner, query, dp);
        }

        public async Task DeleteAsync(int id)
        {
            string query = @"DELETE FROM public.""Owners"" WHERE ""Id""=@Id";
            await DeleteAsync(id, query);
        }

        public async Task<Owner> GetAsync(int id)
        {
            string query = @"SELECT * FROM public.""Owners"" WHERE ""Id"" = @Id";
            return await base.GetAsync(id, query);
        }

        public async Task<Owner> GetOwnerAsync(int id)
        {
            string query = @"SELECT * FROM public.""Owners"" WHERE ""Id"" = @Id";
            return await GetAsync(id, query);
        }



        public async Task UpdateAsync(Owner owner)
        {
            string query = @"UPDATE public.""Owners"" SET
                ""Id"" = @Id,  ""Firstname"" = @Firstname, ""Lastname"" = @Lastname, ""Middlename"" = @Middlename, ""Birthdate"" = @Birthdate, ""Address"" = @Address
                WHERE ""Id"" = @Id";

            var dp = new DynamicParameters();
            dp.Add("Id", owner.Id);
            dp.Add("Firstname", owner.Firstname);
            dp.Add("Lastname", owner.Lastname);
            dp.Add("Middlename", owner.Middlename);
            dp.Add("Birthdate", owner.Birthdate);
            dp.Add("Address", owner.Address);

            await UpdateAsync(owner, query, dp);
        }

        async Task<ActionResult<VirtualizeResponse<Owner>>> IOwnerRepository.GetAllAsync<T>(QueryParameters queryParameters)
        {

            var query = @"SELECT COUNT(*) FROM public.""Owners"";
                SELECT * FROM public.""Owners""
                ORDER BY ""Id"" 
                OFFSET @Skip ROWS FETCH FIRST @Take ROW ONLY";
            return await GetAllAsync<Owner>(queryParameters, query);
        }
    }
}
