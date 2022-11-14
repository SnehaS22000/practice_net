using AutoMapper;
using Dbmodel;
using Entities;
using IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ORM
{
    public class RegisterRepository_ORM : IRegisterRepository
    {
        private ApplicationContext _context;
        private IMapper _mapper;
        public RegisterRepository_ORM(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        public async Task<bool> ChangePassword(int id, string password, string newPassword)
        {
            var user = await _context.RegisteredUsers.FirstOrDefaultAsync(t => t.RegisteredUsersId == id);
            if (user != null)
            {
                user.Password = newPassword;
                _context.Update(user);
                await _context.SaveChangesAsync();

            }

            return true;

        }

        public async Task<bool> Registeruser(Registration user)
        {

            RegistrationModel userRegistration = new RegistrationModel
            {
                Email = user.Email,
                Password = user.Password,
                Name = user.Name,
                Role = "Manager"
            };
            _context.RegisteredUsers.Add(userRegistration);
            await _context.SaveChangesAsync();

            return true;

        }
    }
}
