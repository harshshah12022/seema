using AspCoreApiDapperProject.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AspCoreApiDapperProject.Service
{
   public interface IEmployeeRepository
    {
        Employee execute_sp<Employee>(string query, DynamicParameters sp_params, CommandType commandType = CommandType.StoredProcedure);
        List<Employee> GetAll<Employee>(string query, DynamicParameters sp_params, CommandType commandType = CommandType.StoredProcedure);
    }
}
