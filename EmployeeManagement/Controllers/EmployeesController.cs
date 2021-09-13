using EmployeeManagement.EmployeeData;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace EmployeeManagement.Controllers
{
    [ApiController]
    public class EmployeesController : Controller
    {
        private readonly IEmployeeData _employeeData;
        public EmployeesController(IEmployeeData employeeData)
        {
            _employeeData = employeeData;
        }
        
        
        [HttpGet]
        [Route("api/[controller]")]
        public IActionResult GetEmployees()
        {
            return Ok(_employeeData.GetEmployees());
        }

        [HttpGet]
        [Route("api/[controller]/{id}")]
        public IActionResult GetEmployee(string id)
        {
            var employee = _employeeData.GetEmployee(id);
            if (employee!=null)
            {
                return Ok(employee);
            }            
            return NotFound($"Employee with id: {id} was not found ");                    
        }

        [HttpPost]
        [Route("api/[controller]")]
        public IActionResult AddEmployee(Employee employee)
        {           
            _employeeData.AddEmployee(employee);
            return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + employee.Id, employee);
        }

        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public IActionResult DeleteEmployee(string id)
        {        
            
                _employeeData.DelEmployee(id);
                return Ok();           
            
        }

        [HttpPatch]
        [Route("api/[controller]")]
        public IActionResult EditEmployee(Employee employee)
        {


            _employeeData.EditEmployee(employee);
            return Ok(employee);

        }    

       
    }
}
