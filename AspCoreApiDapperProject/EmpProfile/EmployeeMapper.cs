using AspCoreApiDapperProject.Dtos;
using AspCoreApiDapperProject.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspCoreApiDapperProject.EmpProfile
{
    public class EmployeeMapper:Profile
    {
        public EmployeeMapper()
        {
            CreateMap<Employee, EmployeeDto>();
        }
    }
}
