using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Protocols;
using System.Data;
using Microsoft.Extensions.Configuration;
using System.Data.Common;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Infra.Data
{
    public class DbContext : IDbContext
    {
        private readonly IConfiguration _configuration;
        private readonly string?  _connectionString;
        private readonly string? _databaseName;
        private IDbConnection _dbConnection;

        public DbContext(IConfiguration configuration) {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("SqlConnection");
        }
        
        //Cria a Conexão com o Bancos
        public IDbConnection CreateConnectiosn() => new SqlConnection(_connectionString);

        public void Dispose()
        {

        }
    }
}