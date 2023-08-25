using Dapper;
using Domain.Auth;
using Domain.Auth.Interfaces.Repository;
using Infra.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repository.Auth
{
    internal class UsersRepository : IUsersRepository
    {
        private readonly AppDbContext _context;

        public UsersRepository(AppDbContext context)=> _context = context;

        public Users Add(Users users)
        {
            try
            {
                var query = "INSERT INTO [dbo].[USER] (Id, Username, Password) VALUES (@Id, @Username, @Password)";

                using (var connection = _context.CreateConnectiosn())
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
            try{ 
                var query = "SELECT * FROM [dbo].[USER]";

                using (var connection = _context.CreateConnectiosn())
                {
                        connection.Open();

                        var users =  await connection.QueryAsync<Users>(query);    
                    
                        return users.ToList();
                 }
            }
            catch(Exception ex)
            {
                throw new Exception("Ocorreu um erro ao obter usuários. Detalhes: " + ex.Message, ex);
            }
        }
    }
}
