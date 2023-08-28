using Dapper;
using Domain.Auth;
using Domain.Auth.Interfaces.Repository;
using Infra.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

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
                    string createDatabaseScript = "CREATE TABLE[dbo].[USER]([Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,[Username] VARCHAR(50) NOT NULL,[Password] VARCHAR(50) NOT NULL)";

                    connection.Execute(createDatabaseScript);
                }
            }
        }

        public IDbConnection GetConnections()
        {
            EnsureDatabaseExists();
            return _context.CreateConnections();
        }
        public Users Add(Users users)
        {
            try
            {
                var query = "INSERT INTO [dbo].[USER] (Id, Username, Password) VALUES (@Id, @Username, @Password)";

                using (var connection = GetConnections())
                {
                    connection.Open();

                    connection.Execute(query, new
                    {
                        users.Id,
                        users.Username,
                        users.Password
                    });
                    return users;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao adicionar um usuário. Detalhes: " + ex.Message, ex);
            }
        }

        public async Task<IEnumerable<Users>> GetAll()
        {
            try
            {
                var query = "SELECT * FROM [dbo].[USER]";

                using (var connection = GetConnections())
                {
                    connection.Open();

                    var users = await connection.QueryAsync<Users>(query);

                    return users.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao obter usuários. Detalhes: " + ex.Message, ex);
            }
        }

        public async Task<Users> GetUser(string username)
        {
            try
            {
                var query = "SELECT * FROM [dbo].[USER] WHERE Username = @Username";

                using (var connection = GetConnections())
                {
                    connection.Open();
                    
                    var users = await connection.QueryAsync<Users>(query , new { username});

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
