using Microsoft.Data.SqlClient;
using System.Data;

namespace Infra.Data
{
    public class AppDbContext : IDbContext
    {
        private readonly IConfiguration _configuration;
        private readonly string?  _connectionString;

        public AppDbContext(IConfiguration configuration) {
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