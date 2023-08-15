using Dapper;
using Domain.Auth;
using Domain.Auth.Interfaces.Repository;
using Infra.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repository.Auth
{
    internal class UsersRepository : IUsersRepository
    {
        private readonly DbContext _context;

        public UsersRepository(DbContext context)=> _context = context;

        
        public void Add(Users users)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Users>> GetAll()
        {
            var query = "SELECT * FROM Users";

            using(var connection = _context.CreateConnectiosn())
            {
                var users = await connection.QueryAsync<Users>(query);

                return users.ToList();
            }
        }
    }
}
