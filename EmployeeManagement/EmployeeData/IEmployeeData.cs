using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.EmployeeData
{
    public interface IEmployeeData
    {
        List<Employee> GetEmployees();
        List<Employee> GetEmployee(string Id);
        Employee AddEmployee(Employee employee);
        void DelEmployee(string id);
        Employee EditEmployee(Employee employee);
    }
}
