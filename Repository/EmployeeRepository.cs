using AutoMapper;
using Dbmodel;
using Entities;
using IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using static Google.Protobuf.Reflection.SourceCodeInfo.Types;
using System.Xml.Linq;

namespace Repository
{
    public class EmployeeRepository : BaseRepository, IEmployeeRepository
    {
        private ApplicationContext _context;
        private IMapper _mapper;
        public EmployeeRepository(ApplicationContext context, IMapper mapper, IConfiguration configuration) : base(configuration)
        {
            _context = context;
            _mapper = mapper;

        }

        public async Task<bool> DeleteEmployee(int id)
        {
            List<MySqlParameter> parameters = new()
            {
                new MySqlParameter(parameterName: "id", id)
            };
            int result = await ExecuteNonQuery("sp_DeleteEmployee", parameters);
            return true;

        }


        public async Task<bool> AddNewEmployee(Employee employee)
        {

            List<MySqlParameter> parameters = new()
            {
                new MySqlParameter(parameterName: "Name",employee.Name),
                new MySqlParameter(parameterName:"Designation", employee.Designation),
                new MySqlParameter(parameterName:"Grade", employee.Grade),
                new MySqlParameter(parameterName:"Skills", employee.Skills),
                new MySqlParameter(parameterName:"BenchDuration", employee.BenchDuration),
                new MySqlParameter(parameterName:"Location", employee.Location),
                new MySqlParameter(parameterName:"PsNo", employee.PsNo)

            };
            int result = await ExecuteNonQuery("sp_AddEmployee", parameters);
            return true;

        }



        public async Task<List<Employee>> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();

            DataTable dt = await ExecuteDataTable("sp_FetchEmployees");
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    employees.Add(
                    new Employee
                    {
                        EmployeesDetailId = Convert.ToInt32(dr["EmployeesDetailId"]),
                        Skills = (string)dr["Skills"],
                        Designation = (string)dr["Designation"],
                        BenchDuration = (string)dr["BenchDuration"],
                        PsNo = (int)dr["PsNo"],
                        Location = (string)dr["Location"],
                        Name = (string)dr["Name"],
                        Grade = (string)dr["Grade"],
                    }
                );
                }
            }

            return employees;
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            Employee emp = new();

            List<MySqlParameter> parameters = new()
            {
                new MySqlParameter(parameterName: "id", id)
            };

            //var dt = await ExecuteDataTable("select * from employeesdetail where EmployeesDetailId=@id", parameters);
            var dt = await ExecuteDataTable("sp_FetchEmployeeById", parameters);
            if (dt != null)
            {
                var dr = dt.Rows[0];

                emp.EmployeesDetailId = Convert.ToInt32(dr["EmployeesDetailId"]);
                emp.Skills = (string)dr["Skills"];
                emp.Designation = (string)dr["Designation"];
                emp.BenchDuration = (string)dr["BenchDuration"];
                emp.PsNo = (int)dr["PsNo"];
                emp.Location = (string)dr["Location"];
                emp.Name = (string)dr["Name"];
                emp.Grade = (string)dr["Grade"];
            }

            return emp;
        }

        public async Task<bool> UpdateEmployee(Employee employee)
        { 
            List<MySqlParameter> parameters = new()
            {
                new MySqlParameter(parameterName:"@Name",employee.Name),
                new MySqlParameter(parameterName:"@Designation", employee.Designation),
                new MySqlParameter(parameterName:"@Grade", employee.Grade),
                new MySqlParameter(parameterName:"@Skills", employee.Skills),
                new MySqlParameter(parameterName:"@BenchDuration", employee.BenchDuration),
                new MySqlParameter(parameterName:"@Location", employee.Location),
                new MySqlParameter(parameterName:"@PsNo", employee.PsNo),
                new MySqlParameter(parameterName:"@id",employee.EmployeesDetailId)

            };
            int result = await ExecuteNonQuery("Update employeesdetail set Name=@Name,Designation=@Designation,Grade=@Grade,Skills=@Skills," +
                                               "BenchDuration=@BenchDuration,Location=@Location,PsNo=@PsNo where EmployeesDetailId=@id", parameters);
            return true;
        }

    }
}

