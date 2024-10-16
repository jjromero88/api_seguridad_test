using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace API.SEG.Persistence.Context
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("SegConnection");
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
