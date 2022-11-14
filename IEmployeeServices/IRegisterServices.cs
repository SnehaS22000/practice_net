using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IRegisterServices
    {
        Task<bool> RegisterNewUser(Registration user);
        Task<bool> ChangeUserPassword(int id, string confirmPassword, string password,string newPassword);
    }
}
