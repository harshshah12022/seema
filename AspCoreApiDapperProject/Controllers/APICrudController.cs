using AspCoreApiDapperProject.Models;
using AspCoreApiDapperProject.Service;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace AspCoreApiDapperProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APICrudController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        public APICrudController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        [HttpPost(nameof(Create))]
        public async Task<Employee> Create(Employee data)
        {
            var dp_params = new DynamicParameters();    
            dp_params.Add("EmpName", data.EmpName, DbType.String);
            dp_params.Add("EmpDepartment", data.EmpDepartment, DbType.String);
            dp_params.Add("Email", data.Email, DbType.String);
            dp_params.Add("EmpCode", data.EmpCode, DbType.String);
            dp_params.Add("Age", data.Age, DbType.Int32);
            dp_params.Add("TelephoneNo", data.TelephoneNo, DbType.String);
            dp_params.Add("Gender", data.Gender, DbType.String);
            dp_params.Add("retVal", DbType.String, direction: ParameterDirection.Output);
            var result = await Task.FromResult(_employeeRepository.execute_sp<Employee>("Sp_insert", dp_params, commandType: CommandType.StoredProcedure));
            return result;
        }
        [HttpGet(nameof(GetEmployees))]
        public async Task<Employee> GetEmployees(int page, int pagesize)
        {
            //string result;
           
                //int Totalpage = (page-1) * pagesize;

                //var result = await Task.FromResult(_employeeRepository.GetAll<Employee>($"Select * from Employee order by ID", null, commandType: CommandType.Text));
                var dp_params = new DynamicParameters();
                dp_params.Add("pagenum", page, DbType.Int32);
                dp_params.Add("pagesize", pagesize, DbType.Int32);
                dp_params.Add("retVal", DbType.String, direction: ParameterDirection.Output);
                var result = await Task.FromResult(_employeeRepository.execute_sp<Employee>("Sp_GetEmployee", dp_params, commandType: CommandType.StoredProcedure));
            return result;
        }
       

    [HttpPost(nameof(Update))]
        public async Task<Employee> Update(Employee data)
        {
            var dp_params = new DynamicParameters();
            dp_params.Add("ID", data.ID, DbType.Int32);
            dp_params.Add("EmpName", data.EmpName, DbType.String);
            dp_params.Add("EmpDepartment", data.EmpDepartment, DbType.String);
            dp_params.Add("Email", data.Email, DbType.String);
            dp_params.Add("EmpCode", data.EmpCode, DbType.String);
            dp_params.Add("Age", data.Age, DbType.Int32);
            dp_params.Add("TelephoneNo", data.TelephoneNo, DbType.String);
            dp_params.Add("Gender", data.Gender, DbType.String);
            dp_params.Add("retVal", DbType.String, direction: ParameterDirection.Output);
            var result = await Task.FromResult(_employeeRepository.execute_sp<Employee>("[dbo].[Sp_Update]"
                , dp_params,
                commandType: CommandType.StoredProcedure));
            return result;
        }
        [HttpDelete(nameof(Delete))]
        public async Task<Employee> Delete(int Id)
        {
            var dp_params = new DynamicParameters();
            dp_params.Add("Id", Id, DbType.Int32);
            dp_params.Add("retVal", DbType.String, direction: ParameterDirection.Output);
            var result = await Task.FromResult(_employeeRepository.execute_sp<Employee>("[dbo].[Sp_Delete]"
                , dp_params,
                commandType: CommandType.StoredProcedure));
            return result;
        }
    }
}
