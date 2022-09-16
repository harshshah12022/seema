using AspCoreApiDapperProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspCoreApiDapperProject.Service
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetAllEmployee();
        Task<Employee> GetByIDEmployee(int Id);
        Task<Employee> CreateEmployee(Employee employee);
        Task<Employee> UpdateEmployee(Employee employee);
        Task<Employee> DeleteEmployee(int id);
        Task<Employee> SearchEmployee(string empName);
        //Task<Employee> GetAllEmployeeData(int page, int pageseze);
    }
}
