using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IUserServices
    {
        Task<Registration> GetUserDetail(Login user);
        Task<Registration> GetUserById(int id);
    }
}
