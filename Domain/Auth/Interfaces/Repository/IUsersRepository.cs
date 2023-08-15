using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Auth.Interfaces.Repository
{
    public interface IUsersRepository
    {
        void Add(Users users);

        public Task<IEnumerable<Users>> GetAll();
    }
}
