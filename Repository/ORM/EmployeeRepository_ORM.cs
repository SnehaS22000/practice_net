using AutoMapper;
using Dbmodel;
using Entities;
using IRepository;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace Repository.ORM
{
    public class EmployeeRepository_ORM : IEmployeeRepository
    {
        private ApplicationContext _context;
        private IMapper _mapper;
        public EmployeeRepository_ORM(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> DeleteEmployee(int id)
        {
            var employeeFind = await _context.EmployeesDetail.FirstOrDefaultAsync(a => a.EmployeesDetailId == id);
            if (employeeFind != null)
            {
                _context.EmployeesDetail.Remove(employeeFind);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }


        public async Task<bool> AddNewEmployee(Employee employee)
        {
            var EmployeeFind = await _context.EmployeesDetail.FirstOrDefaultAsync(a => a.PsNo == employee.PsNo);

            if (EmployeeFind == null)
            {
                EmployeeModel emp = new EmployeeModel
                {
                    PsNo = employee.PsNo,
                    Skills = employee.Skills,
                    Name = employee.Name,
                    Location = employee.Location,
                    BenchDuration = employee.BenchDuration,
                    Grade = employee.Grade,
                    Designation = employee.Designation
                };

                _context.EmployeesDetail.Add(emp);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<Employee>> GetEmployees()
        {
            var employeeList = await _context.EmployeesDetail.ToListAsync();
            return _mapper.Map<List<Employee>>(employeeList);
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            var employee = await _context.EmployeesDetail.FirstOrDefaultAsync(a => a.EmployeesDetailId == id); ;
            return _mapper.Map<Employee>(employee);

        }

        public async Task<bool> UpdateEmployee(Employee employee)
        {

            EmployeeModel emp = new EmployeeModel
            {
                PsNo = employee.PsNo,
                EmployeesDetailId = employee.EmployeesDetailId,
                Skills = employee.Skills,
                Name = employee.Name,
                Location = employee.Location,
                BenchDuration = employee.BenchDuration,
                Grade = employee.Grade,
                Designation = employee.Designation
            };

            _context.EmployeesDetail.Update(emp);
            await _context.SaveChangesAsync();
            return true;

        }
    }
}
