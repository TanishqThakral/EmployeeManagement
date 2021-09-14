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
            SqlCommand cmd = new SqlCommand("spGetEmployees",con);
            cmd.CommandType = CommandType.StoredProcedure;
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
            SqlCommand cmd = new SqlCommand("spAddEmployee", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("Id", employee.Id);
            cmd.Parameters.AddWithValue("Name", employee.Name);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            return employee;
        }

        public void DelEmployee(string id)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            SqlCommand cmd = new SqlCommand("spDeleteEmployee", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("Id", id);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public Employee EditEmployee(Employee employee)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            SqlCommand cmd = new SqlCommand("spEditEmployee", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("Id", employee.Id);
            cmd.Parameters.AddWithValue("Name", employee.Name);
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

