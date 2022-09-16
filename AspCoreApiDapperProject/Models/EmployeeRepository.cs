using AspCoreApiDapperProject.Service;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AspCoreApiDapperProject.Models
{
    public class EmployeeRepository:IEmployeeService
    {
        private readonly string conStr;
       // private IConfiguration  Configuration;
        public EmployeeRepository(IConfiguration Configuration)
        {
            conStr = Configuration.GetConnectionString("ConnectionDB");
            //conStr = @"Server=tcp:dbserver06102021.database.windows.net,1433;Initial Catalog=CorePoCDatabase;Persist Security Info=False;User ID=rootuser;Password=Admin@2021!@#$%;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        }
        public IDbConnection connection
        {
            get
            {
                return new SqlConnection(conStr);
            }
        }

        public async Task<List<Employee>> GetAllEmployee()
        {
            int page = 2; int pagesize = 5;
            DynamicParameters dynamicParameters = new();
            dynamicParameters.Add("@pagenum", page);
            dynamicParameters.Add("@pagesize", pagesize);
            var result = await connection.QueryMultipleAsync("Sp_GetEmployee", dynamicParameters, null, null, CommandType.StoredProcedure);

            List<Employee> patientValues = result.ReadAsync<Employee>().Result.ToList();

            return patientValues;

        }
        //public async Task<List<Employee>> GetAllEmployee()
        //{
        //    using (IDbConnection dbConnection = connection)
        //    {
        //        string sql = @"SELECT * FROM EMPLOYEE ORDER BY ID";
        //        dbConnection.Open();
        //        return (List<Employee>)dbConnection.Query<Employee>(sql);
        //    }
        //    //throw new System.NotImplementedException();
        //}

        public async Task<Employee> SearchEmployee(string empName)
        {
            using (IDbConnection dbConnection = connection)
            {
                string sql = @"SELECT * FROM EMPLOYEE WHERE EmpName LIKE @empname";
                dbConnection.Open();
                return dbConnection.Query<Employee>(sql, new { EmpName = empName }).FirstOrDefault();
            }
            //throw new System.NotImplementedException();
        }
        public async Task<Employee> GetByIDEmployee(int Id)
        {
            using (IDbConnection dbConnection = connection)
            {
                string sql = @"SELECT * FROM EMPLOYEE WHERE ID=@id ";
                dbConnection.Open();
                return dbConnection.Query<Employee>(sql, new { ID = Id }).FirstOrDefault();
            }
            //throw new System.NotImplementedException();
        }

        public async Task<Employee> CreateEmployee(Employee employee)
        {
            using (IDbConnection dbconnection = connection)
            {
                string sql = @"INSERT INTO EMPLOYEE(EmpName,EmpDepartment,Email,EmpCode,Gender,TelephoneNo,Age)
                            VALUES(@EmpName,@EmpDepartment,@Email,@EmpCode,@Gender,@TelephoneNo,@Age)";
                dbconnection.Open();
                dbconnection.Execute(sql, employee);
                dbconnection.Close();
            }
            return employee;
            //throw new System.NotImplementedException();
        }

        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            using (IDbConnection dbConnection = connection)
            {
                string sql = @"UPDATE EMPLOYEE SET EmpName=@EmpName, EmpDepartment=@EmpDepartment, Email=@Email,
                          EmpCode=@EmpCode WHERE ID=@id ";
                dbConnection.Open();
                dbConnection.Query(sql, employee);
                dbConnection.Close();
            }
            return employee;
            //throw new System.NotImplementedException();
        }

        public async Task<Employee> DeleteEmployee(int id)
        {
            using (IDbConnection dbConnection = connection)
            {
                string sql = @"DELETE FROM EMPLOYEE WHERE ID=@id ";
                dbConnection.Open();
                dbConnection.Query(sql, new { ID = id });
                dbConnection.Close();
                return dbConnection.Query<Employee>(sql, new { ID = id }).FirstOrDefault();
            }
            // throw new System.NotImplementedException();
        }
    }
}
