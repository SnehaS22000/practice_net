using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepository
{
    public interface IUserRepository
    {
        Task<Registration> GetUser(Login user);
        Task<Registration> GetUserById(int Id);
    }
}
