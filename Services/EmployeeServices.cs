using Dbmodel;
using Entities;
using IRepository;
using Microsoft.EntityFrameworkCore;
using Services.Contracts;

namespace Services
{
    public class EmployeeServices : IEmployeeServices
    {

        private IEmployeeRepository _employeeRepository;
        public EmployeeServices(IEmployeeRepository employeeRepository)
        {

            _employeeRepository = employeeRepository;
        }

        public async Task<bool> AddEmployee(Employee details)
        {
            return await _employeeRepository.AddNewEmployee(details);
            
        }

        public async Task<bool> DeleteEmployee(int id)
        {


            var res = await _employeeRepository.DeleteEmployee(id);
            return res;


        }


        public async Task<List<Employee>> GetEmployees()
        {
            var res = await _employeeRepository.GetEmployees();
            return res;
        }



        public async Task<Employee> GetEmployeeById(int id)
        {

            var res = await _employeeRepository.GetEmployeeById(id);
            return res;


        }

        public async Task<bool> UpdateEmployee(Employee modifiedDetails)
        {

            var res = await _employeeRepository.UpdateEmployee(modifiedDetails);
            return res;


        }
    }
}
