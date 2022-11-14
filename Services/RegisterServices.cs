using AutoMapper;
using Common;
using Dbmodel;
using Entities;
using IRepository;
using Microsoft.EntityFrameworkCore.Internal;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class RegisterServices : IRegisterServices
    {
        private IRegisterRepository _registerRepository;
        private IMapper _mapper;
        private IUserServices _userServices;
        public RegisterServices(IRegisterRepository registerRepository, IMapper mapper, IUserServices userServices)
        {
            _registerRepository = registerRepository;
            _mapper = mapper;
            _userServices = userServices;
        }

        public async Task<bool> ChangeUserPassword(int id, string password, string confirmPassword, string newPassword)
        {
            if (password != null && password == confirmPassword)
            {
                var userData = await _userServices.GetUserById(id);
                string PasswordHash = PasswordHashHelper.HashPassword(password, userData.Salt);
                if (userData.Password == PasswordHash)
                {
                    string newPasswordHash = PasswordHashHelper.HashPassword(newPassword, userData.Salt);
                    var res = await _registerRepository.ChangePassword(id, password, newPasswordHash);
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> RegisterNewUser(Registration user)
        {
            string salt = PasswordHashHelper.GenerateSalt();
            user.Salt = salt;
            string passwordHash = PasswordHashHelper.HashPassword(user.Password, salt);
            user.Password = passwordHash;
            if (user.Email != null && user.Password != null)
            {
                var res = await _registerRepository.Registeruser(user);
                return true;
            }
            return false;

        }

    }
}
