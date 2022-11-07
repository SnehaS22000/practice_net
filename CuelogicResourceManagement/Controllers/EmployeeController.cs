
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using MySqlX.XDevAPI.Common;
using Services.Contracts;
using System.Data;

namespace CuelogicResourceManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private IEmployeeServices _employeeServices;
        private ICacheHelper _cacheHelper;
       
        public EmployeeController(IEmployeeServices employeeServices,ICacheHelper cacheHelper)
        {

            _employeeServices = employeeServices;
            _cacheHelper = cacheHelper;

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveEmployee(int id)
        {

            var result = await _employeeServices.DeleteEmployee(id);
            if (result == true)
            {
                return Ok(new ApiResponse<Employee>
                {
                    Message = "Employee is Deleted Successfully",

                    Success = true
                });
            }
            else
            {
                return BadRequest(new ApiResponse<Employee>
                {
                    Message = "Employee is Deleted Successfully",

                    Success = false
                });
            }

        }



        [HttpGet]
        public async Task<IActionResult> FetchEmployees()
        {
            
            var employees = _cacheHelper.Get<List<Employee>>("FetchEmployees");
            if (employees == null)
            {
                var result = await _employeeServices.GetEmployees();
                _cacheHelper.Add("FetchEmployees", result);
                if (result != null)
                {
                    return Ok(new ApiResponse<List<Employee>>
                    {
                        Result = result,
                        Success = true,
                        Message = "Employee List"
                    }
                      );
                }
                return BadRequest(new ApiResponse<List<Employee>>
                {

                    Success = false,
                    Message = "Cannot Get Employee List"
                }
                );
            }

            return Ok(new ApiResponse<List<Employee>>
            {
                Result = (List<Employee>)employees,
                Success = true,
                Message = "Employee List"
            }
              );

        }



        [HttpGet("{id}")]
        public async Task<IActionResult> FetchEmployees(int id)
        {
            var employee = _cacheHelper.Get<Employee>("FetchEmployeesById"+ id);
            if (employee == null)
            {
                var result = await _employeeServices.GetEmployeeById(id);
                _cacheHelper.Add("FetchEmployeesById"+id, result);
                return Ok(new ApiResponse<Employee>
                {
                    Result = result,
                    Success = true,
                    Message = "Employee Details"
                }
           );
            }
            return Ok(new ApiResponse<Employee>
            {
                Result = employee,
                Success = true,
                Message = "Employee Details"
            }
            );

        }

        [HttpPut]
        public async Task<IActionResult> ModifyDetails(Employee modifiedDetails)
        {
            var result = await _employeeServices.UpdateEmployee(modifiedDetails);
            if (result == true)
            {
                return Ok(new ApiResponse<Employee>
                {

                    Success = true,
                    Message = "Details Updated"
                });
            }
            else
            {
                return BadRequest(new ApiResponse<Employee>
                {

                    Success = false,
                    Message = "Update Failed"
                });
            }
        }

        [HttpPost]

        public async Task<IActionResult> AddEmployee(Employee details)
        {


            var result = await _employeeServices.AddEmployee(details);
            if (result == true)
            {
                return Ok(new ApiResponse<Employee>
                {

                    Success = true,
                    Message = "Employee Details Added"
                });
            }
            else
            {
                return BadRequest(new ApiResponse<Employee>
                {

                    Success = true,
                    Message = "Failed to add Employee"
                });
            }
        }
    }
}
