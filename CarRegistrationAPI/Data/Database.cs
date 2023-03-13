using Dapper;
using Npgsql;
using System.Data.SqlClient;

namespace CarRegistrationAPI.Data
{
    public static class Database
    {
        public static void EnsureDatabase(string connectionString, string name)
        {
            var parameters = new DynamicParameters();
            parameters.Add("name", name);
            using var connection = new NpgsqlConnection(connectionString);
            var records = connection.Query("SELECT 1 FROM pg_database WHERE datname= @name",
                 parameters);
            if (!records.Any())
            {
                connection.Execute($"CREATE DATABASE {name}");
            }
        }
    }
}
