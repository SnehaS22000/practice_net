using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dbmodel
{
    public class ApplicationContext:DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<RegistrationModel> RegisteredUsers { get; set; }
        public DbSet<EmployeeModel> EmployeesDetail { get; set; }
        public DbSet<ExceptionModel> ExceptionDetails { get; set; } 
    }
}
