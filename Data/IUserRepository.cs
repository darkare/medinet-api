using MedinetAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedinetAPI.Data
{
    interface IUserRepository: IDisposable
    {
        IEnumerable<User> GetUsers();
        User GetUserById(int userId);
    }
}
