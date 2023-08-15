using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Protocols;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace Infra.Data
{
    public class DbContext : IDbContext
    {
        private readonly IConfiguration _configuration;
        private readonly string?  _connectionString;
        
        public DbContext(IConfiguration configuration) {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("SqlConnection");
        }

        public IDbConnection CreateConnectiosn() => new SqlConnection(_connectionString);

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}