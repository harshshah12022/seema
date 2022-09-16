using AspCoreApiDapperProject.Dtos;
using AspCoreApiDapperProject.Models;
using AspCoreApiDapperProject.Service;
using AutoMapper;
using CoreApiResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AspCoreApiDapperProject.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : BaseController
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;
        private readonly IMapper _mapper;
        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger, IMapper mapper)
        {
            _employeeService = employeeService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]

        //Get All
        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                //int totalpage = (page - 1) * pagesize;
                _logger.LogInformation("This is just a log in GetAll()");
                var result = await _employeeService.GetAllEmployee();
                var emp = _mapper.Map<List<Employee>>(result);
               
                return Ok(emp);

            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        [HttpGet]
        [Route("Search")]
        public async Task<IActionResult> Search(string text)
        {
            try
            {
                _logger.LogInformation("This is just a log in Search()");
                var result = await _employeeService.SearchEmployee(text);
                var emp = _mapper.Map<EmployeeDto>(result);
                return Ok(emp);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }

        }

        //Get BY ID
        [HttpPost]
        [Route("Get/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                _logger.LogInformation("This is just a log in GetById()");
                var result = await _employeeService.GetByIDEmployee(id);
                var emp = _mapper.Map<EmployeeDto>(result);
                if (result == null) return NotFound();

                //return result;
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }

        }


        //INSERT
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Employee employee)
        {
            try
            {
                _logger.LogInformation("This is just a log in Create()");
                var result = await _employeeService.CreateEmployee(employee);
                var emp = _mapper.Map<EmployeeDto>(result);

                return CustomResult("Data Loaded Successfully",emp);

            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }

        }

        //UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] Employee employee)
        {
            try
            {
                _logger.LogInformation("This is just a log in Update()");
                var result = await _employeeService.UpdateEmployee(employee);
                var emp = _mapper.Map<EmployeeDto>(result);
                //if (result == null) return NotFound();
                return CustomResult("Updated Successfully",emp);

            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }

        }

        //Delete
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                _logger.LogInformation("This is just a log in Delete()");
                var result = await _employeeService.DeleteEmployee(id);
                var emp = _mapper.Map<EmployeeDto>(result);
                //if (result! == null) return NotFound();
                return Ok(emp);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }

        }

    }
}
