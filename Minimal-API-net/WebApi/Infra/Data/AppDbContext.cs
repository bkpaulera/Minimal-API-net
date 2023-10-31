using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WebApi.Domain.Auth.Models;

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
        public IDbConnection CreateConnections() => new SqlConnection(_connectionString);


        public void Dispose()
        {

        }
    }
}