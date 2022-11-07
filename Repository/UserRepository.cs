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
    public class UserRepository:BaseRepository,IUserRepository
    {
        private ApplicationContext _context;
        public UserRepository(ApplicationContext context, IConfiguration configuration) : base(configuration)
        {
            _context = context; 
        }

        public async Task<Registration>GetUser(Login user)
        {
            Registration registration = new();

            List<MySqlParameter> parameters = new()
            {
                new MySqlParameter(parameterName: "@Email", user.Email)
            };

            var dt = await ExecuteDataTable("select * from RegisteredUsers where Email=@Email", parameters);
            if (dt != null)
            {
                var dr = dt.Rows[0];

                registration.RegisteredUsersId = Convert.ToInt32(dr["RegisteredUsersId"]);
                registration.Name = ((string)dr["Name"]);
                registration.Email = ((string)dr["Email"]);
                registration.Password = ((string)dr["Password"]);
                registration.Role = ((string)dr["Role"]);
                registration.Salt = ((string)dr["Salt"]);
            }        
            return registration;
        }



        public async Task<Registration> GetUserById(int Id)
        {
            Registration registration= new();

            List<MySqlParameter> parameters = new()
            {
                new MySqlParameter(parameterName: "@id", Id)
            };

            var dt = await ExecuteDataTable("select * from RegisteredUsers where RegisteredUsersId=@Id", parameters);
            if (dt != null)
            {
                var dr = dt.Rows[0];

                registration.RegisteredUsersId = Convert.ToInt32(dr["RegisteredUsersId"]);
                registration.Name = ((string)dr["Name"]);
                registration.Email = ((string)dr["Email"]);
                registration.Password = ((string)dr["Password"]);
                registration.Role = ((string)dr["Role"]);
                registration.Salt = ((string)dr["Salt"]);
            }

            return registration ;
        }
    }
}
