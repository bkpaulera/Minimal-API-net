using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Auth.Interfaces.Repository
{
    public interface IUsersRepository
    {
        public Users Add(Users users);
        public Task<IEnumerable<Users>> GetAll();
        public Task<Users> GetUser(string Username);
        public Task<bool> PutUserPassword(string id, string newPassword);
        public Task<bool> DeleteUser(string id);
        public void EnsureDatabaseExists();
    }
}
