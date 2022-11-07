using AutoMapper;
using Dbmodel;
using Entities;
using IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RegisterRepository : BaseRepository, IRegisterRepository
    {

        private IMapper _mapper;
        public RegisterRepository(IMapper mapper, IConfiguration configuration) : base(configuration)
        {

            _mapper = mapper;

        }

        public async Task<bool> ChangePassword(int id, string password, string newPassword)
        {
            List<MySqlParameter> parameters = new()
            {
                new MySqlParameter(parameterName:"@Password", newPassword),
                 new MySqlParameter(parameterName:"@id", id)

            };
            int result = await ExecuteNonQuery("Update RegisteredUsers set Password=@password where RegisteredUsersId=@id", parameters);
            return true;
        }

        public async Task<bool> Registeruser(Registration user)
        {
            List<MySqlParameter> parameters = new()
            {
                new MySqlParameter(parameterName:"@Name", user.Name),
                 new MySqlParameter(parameterName:"@Email", user.Email),
                 new MySqlParameter(parameterName:"@Password", user.Password),
                 new MySqlParameter(parameterName:"@Role", "Manager"),
                 new MySqlParameter(parameterName:"@Salt", user.Salt)
            };
            int result = await ExecuteNonQuery("INSERT INTO RegisteredUsers (Name,Email,Password,Role,Salt) " +
                    "VALUES (@Name,@Email,@Password,@Role,@Salt) ", parameters);
            return true;
        }
    }
}
