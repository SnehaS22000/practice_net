using Entities;
using IRepository;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UserServices:IUserServices
    {
        private IUserRepository _userRepository;
        public UserServices(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Registration> GetUserDetail(Login user)
        {
            
            var userData=await _userRepository.GetUser(user);
            Registration model = new Registration();
            model.Email = userData.Email;
            model.Role = userData.Role;
            model.RegisteredUsersId = userData.RegisteredUsersId;
            model.Password = userData.Password;
            model.Name = userData.Name;
            model.Salt = userData.Salt;

            return model;
        }
        public async Task<Registration> GetUserById(int id)
        {

            var userData = await _userRepository.GetUserById(id);
            Registration model = new Registration();
            model.Email = userData.Email;
            model.Role = userData.Role;
            model.RegisteredUsersId = userData.RegisteredUsersId;
            model.Password = userData.Password;
            model.Name = userData.Name;
            model.Salt = userData.Salt;

            return model;
        }

    }
}
