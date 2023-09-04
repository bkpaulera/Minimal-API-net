using WebApi.Domain.Auth.Models;

namespace Domain.Auth.Interfaces.Repository
{
    public interface IUsersRepository
    {
        public Task<bool> Add(UserEntitie users);
        public Task<IEnumerable<UserEntitie>> GetAll();
        public Task<UserEntitie> GetUser(string Username);
        public Task<bool> PutUserPassword(string id, string newPassword);
        public Task<bool> DeleteUser(string id);
        public void EnsureDatabaseExists();
    }
}
