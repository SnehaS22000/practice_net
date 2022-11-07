using Entities;

namespace IRepository
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetEmployees();
        Task<Employee> GetEmployeeById(int id);

        Task<bool> AddNewEmployee(Employee details);

        Task<bool> DeleteEmployee(int id);

        Task<bool> UpdateEmployee(Employee modifiedDetails);
    }
}