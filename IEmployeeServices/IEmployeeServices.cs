using Entities;

namespace Services.Contracts
{
    public interface IEmployeeServices
    {
        Task<List<Employee>> GetEmployees();
        Task<Employee> GetEmployeeById(int id);

        Task<bool> AddEmployee(Employee details);

        Task<bool> DeleteEmployee(int id);

        Task<bool> UpdateEmployee(Employee modifiedDetails);
    }
}