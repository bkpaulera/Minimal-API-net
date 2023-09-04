using Dapper;
using Domain.Auth.Interfaces.Repository;
using Infra.Data;
using System.Data;
using WebApi.Domain.Auth.Models;

namespace Infra.Repository.Auth
{
    internal class UsersRepository : IUsersRepository
    {
        private readonly AppDbContext _context;

        public UsersRepository(AppDbContext context) => _context = context;
        public void EnsureDatabaseExists()
        {
            using (var connection = _context.CreateConnections())
            {
                connection.Open();

                // Verificar se o banco de dados já existe
                var databaseExists = connection.ExecuteScalar<int>("SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'USER'");

                if (databaseExists == 0)
                {
                    // Criar o banco de dados usando um script
                    string createDatabaseScript = "CREATE TABLE [dbo].[USER] ([Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, [Username] NVARCHAR(50) NULL,[Password] NVARCHAR(50) NULL, [Image] NVARCHAR(100) NULL,[Email] NVARCHAR(255) NULL, CONSTRAINT UQ_Id UNIQUE ([Id]),CONSTRAINT UQ_Username UNIQUE ([Username]), CONSTRAINT UQ_Email UNIQUE ([Email]));";

                    connection.Execute(createDatabaseScript);
                }
            }
        }

        public IDbConnection GetConnections()
        {
            EnsureDatabaseExists();
            return _context.CreateConnections();
        }

        public async Task<bool> Add(UserEntitie users)
        {
            try
            {
                var query = "INSERT INTO [dbo].[USER] (Id, Username, Password,Image,Email) VALUES (@Id, @Username, @Password,@Image,@Email)";

                using (var connection = GetConnections())
                {
                    connection.Open();

                   var rowsAffected = connection.Execute(query, new
                    {
                        users.Id,
                        users.Username,
                        users.Password,
                        users.Image,
                        users.Email,
                    });

                    return rowsAffected>0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao adicionar um usuário. Detalhes: " + ex.Message, ex);
            }
        }

        public async Task<IEnumerable<UserEntitie>> GetAll()
        {
            try
            {
                var query = "SELECT * FROM [dbo].[USER]";

                using (var connection = GetConnections())
                {
                    connection.Open();

                    var users = await connection.QueryAsync<UserEntitie>(query);

                    return users.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao obter usuários. Detalhes: " + ex.Message, ex);
            }
        }

        public async Task<UserEntitie> GetUser(string username)
        {
            try
            {
                var query = "SELECT * FROM [dbo].[USER] WHERE Username = @Username";

                using (var connection = GetConnections())
                {
                    connection.Open();
                    
                    var users = await connection.QueryAsync<UserEntitie>(query , new { username});

                    if (users == null || !users.Any())
                    {
                        throw new Exception("Senha ou Usuário não estão corretos");
                    }

                    return users.First();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao obter usuários. Detalhes: " + ex.Message, ex);
            }
        }

        public async Task<bool> PutUserPassword(string id, string newPassword)
        {
            try
            {
                var query = "UPDATE [dbo].[USER] SET Password  = @newPassword WHERE id = @id";

                using (var connection = GetConnections())
                {
                    connection.Open();

                    int rowsAffected = await connection.ExecuteAsync(query, new { NewPassword = newPassword, Id = id });

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao obter usuários. Detalhes: " + ex.Message, ex);
            }
        }

        public async Task<bool> DeleteUser(string id)
        {
            try
            {
                var query = "DELETE FROM [dbo].[USER] WHERE id = @id";

                using (var connection = GetConnections())
                {
                    connection.Open();

                    int rowsAffected = await connection.ExecuteAsync(query, new { Id = id });

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao obter usuários. Detalhes: " + ex.Message, ex);
            }
        }
    }
}
