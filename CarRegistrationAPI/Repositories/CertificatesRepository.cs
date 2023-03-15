using CarRegistrationAPI.Context;
using CarRegistrationAPI.Entities;
using Dapper;
using Microsoft.AspNetCore.Mvc;

namespace CarRegistrationAPI.Repositories
{
    public class CertificatesRepository : GenericRepository<Certificate>, ICertificateRepository
    {
        public CertificatesRepository(DapperContext context) : base(context)
        {

        }

        public async Task<Certificate> AddAsync(Certificate certificate)
        {
            string query = @"INSERT INTO public.""Certificates"" (""Ownerid"", ""Registrationplate"", ""Model"", ""Year"", ""Color"", ""Vin"", ""Type"", ""Maxmass"", ""Issuedate"", ""Issuer"") VALUES(@Ownerid, @Registrationplate, @Model, @Year, @Color, @Vin, @Type, @Maxmass, @Issuedate, @Issuer)";
            var dp = new DynamicParameters();
            dp.Add("Ownerid", certificate.Ownerid);
            dp.Add("Registrationplate", certificate.Registrationplate);
            dp.Add("Model", certificate.Model);
            dp.Add("Year", certificate.Year);
            dp.Add("Color", certificate.Color);
            dp.Add("Vin", certificate.Vin);
            dp.Add("Type", certificate.Type);
            dp.Add("Maxmass", certificate.Maxmass);
            dp.Add("Issuedate", certificate.Issuedate);
            dp.Add("Issuer", certificate.Issuer);

            return await base.AddAsync(certificate, query, dp);
        }

        public async Task DeleteAsync(int id)
        {
            string query = @"DELETE FROM public.""Certificates"" WHERE ""Id""=@Id";
            await DeleteAsync(id, query);
        }

        public async Task<Certificate> GetAsync(int id)
        {
            string query = @"SELECT * FROM public.""Certificates"" WHERE ""Id"" = @Id";
            return await base.GetAsync(id, query);
        }

        public async Task<Certificate> GetCertificateAsync(int id)
        {
            string query = @"SELECT * FROM public.""Certificates"" WHERE ""Id"" = @Id";
            return await GetAsync(id, query);
        }



        public async Task UpdateAsync(Certificate certificate)
        {
            string query = @"UPDATE public.""Certificates"" SET
                ""Id"" = @Id,  ""Ownerid"" = @Ownerid, ""Registrationplate"" = @Registrationplate, ""Model"" = @Model, ""Year"" = @Year, ""Color"" = @Color, ""Vin"" = @Vin, ""Type"" = @Type, ""Maxmass"" = @Maxmass, ""Issuedate"" = @Issuedate, ""Issuer"" = @Issuer
                WHERE ""Id"" = @Id";

            var dp = new DynamicParameters();
            dp.Add("Ownerid", certificate.Ownerid);
            dp.Add("Registrationplate", certificate.Registrationplate);
            dp.Add("Model", certificate.Model);
            dp.Add("Year", certificate.Year);
            dp.Add("Color", certificate.Color);
            dp.Add("Vin", certificate.Vin);
            dp.Add("Type", certificate.Type);
            dp.Add("Maxmass", certificate.Maxmass);
            dp.Add("Issuedate", certificate.Issuedate);
            dp.Add("Issuer", certificate.Issuer);

            await UpdateAsync(certificate, query, dp);
        }

        async Task<ActionResult<VirtualizeResponse<Certificate>>> ICertificateRepository.GetAllAsync<T>(QueryParameters queryParameters)
        {

            var query = @"SELECT COUNT(*) FROM public.""Certificates"";
                SELECT * FROM public.""Certificates""
                ORDER BY ""Id"" 
                OFFSET @Skip ROWS FETCH FIRST @Take ROW ONLY";
            return await GetAllAsync<Certificate>(queryParameters, query);
        }
    }
}
