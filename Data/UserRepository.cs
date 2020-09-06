using MedinetAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedinetAPI.Data
{
    public class UserRepository : IUserRepository
    {
        private BaseDbContext _dbContext;
        private bool disposed = false;

        public UserRepository(BaseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<User> GetUsers()
        {
            return _dbContext.Users; ;
        }
        public User GetUserById(int userId)
        {
            var user = _dbContext.Users.SingleOrDefault(m => m.Id == userId);
            return user;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
