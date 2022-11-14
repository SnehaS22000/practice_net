using Dbmodel;
using Entities;
using IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ORM
{
    public class UserRepository_ORM : IUserRepository
    {
        private ApplicationContext _context;
        public UserRepository_ORM(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Registration> GetUser(Login user)
        {
            Registration model = new Registration();
            var userData = await _context.RegisteredUsers.FirstOrDefaultAsync(t => t.Email == user.Email);
            model.Email = userData.Email;
            model.Role = userData.Role;
            model.RegisteredUsersId = userData.RegisteredUsersId;
            model.Password = userData.Password;
            model.Name = userData.Name;
            return model;

        }

        public Task<Registration> GetUserById(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
