using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepository
{
    public interface IRegisterRepository
    {
        /// <summary>
        /// Register User
        /// </summary>
        /// <param name="user">Registration Model</param>
        /// <returns>Bool</returns>
        Task<bool> Registeruser(Registration user);
        Task<bool> ChangePassword(int id, string password,string newPassword);
    }
}
