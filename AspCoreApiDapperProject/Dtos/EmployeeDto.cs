using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspCoreApiDapperProject.Dtos
{
    public class EmployeeDto
    {
        public int ID { get; set; }
        public string EmpName { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string EmpDepartment { get; set; }
        public string Email { get; set; }
        public string TelephoneNo { get; set; }
        public string EmpCode { get; set; }
    }
}
