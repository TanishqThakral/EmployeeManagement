using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace EmployeeManagement.EmployeeData
{
    public class SqlEmployeeService : IEmployeeData
    {
        private readonly IConfiguration _configuration;

        public SqlEmployeeService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private List<Employee> LoadList()
        { 
            List<Employee> employees = new List<Employee>();

            SqlConnection con= new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            SqlCommand cmd = new SqlCommand("Select * from Employee",con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            for(int i=0; i< dt.Rows.Count; i++)
            {
                Employee employee = new Employee();
                employee.Id = dt.Rows[i]["Id"].ToString();
                employee.Name = dt.Rows[i]["Name"].ToString();
                employees.Add(employee);
            }

            return employees;
        }         
       

        public Employee AddEmployee(Employee employee)
        {
            employee.Id = Guid.NewGuid().ToString();          

            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            SqlCommand cmd = new SqlCommand("Insert into Employee values ('" + employee.Id + "' , '" + employee.Name + "')",con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            return employee;
        }

        public void DelEmployee(string id)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            SqlCommand cmd = new SqlCommand("Delete from Employee where Id = '" + id + "'", con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public Employee EditEmployee(Employee employee)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            SqlCommand cmd = new SqlCommand("UPDATE Employee SET Name ='" +employee.Name + "' WHERE Id = '" + employee.Id + "'", con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return employee;
        }

        public List<Employee> GetEmployee(string id)
        {         
            return LoadList().Where(e => e.Id == id).ToList();
        }

        public List<Employee> GetEmployees()
        {
            return LoadList();
        }
    }
}

