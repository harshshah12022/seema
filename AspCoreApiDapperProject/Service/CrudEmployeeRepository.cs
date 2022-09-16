using AspCoreApiDapperProject.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AspCoreApiDapperProject.Service
{
    public class CrudEmployeeRepository : IEmployeeRepository
    {
        private readonly IConfiguration _configuration;
        public CrudEmployeeRepository(IConfiguration Configuration)
        {
            _configuration = Configuration;
        }
        public Employee execute_sp<Employee>(string query, DynamicParameters sp_params, CommandType commandType = CommandType.StoredProcedure)
        {
            Employee result;
            using (IDbConnection dbConnection = new SqlConnection(_configuration.GetConnectionString("ConnectionDB")))
            {
                if (dbConnection.State == ConnectionState.Closed)
                    dbConnection.Open();
                using var transaction = dbConnection.BeginTransaction();
                try
                {
                    dbConnection.Query<Employee>(query, sp_params, commandType: commandType, transaction: transaction);
                    result = sp_params.Get<Employee>("retVal"); //get output parameter value
                    //result = GetAll<Employee>(query);
                    transaction.Commit();
                    
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                    //return CustomResult()
                }
            };
            return result;
            //throw new NotImplementedException();
        }

        public List<Employee> GetAll<Employee>(string query, DynamicParameters sp_params, CommandType commandType = CommandType.StoredProcedure)
        {
            using IDbConnection db = new SqlConnection(_configuration.GetConnectionString("ConnectionDB"));
            return db.Query<Employee>(query, sp_params, commandType: commandType).ToList();
            //throw new NotImplementedException();
        }
    }
}
