using MVCApp.Models;
using System.Data.SqlClient;

namespace MVCApp
{
    public class EmployeeRepository
    {
        private readonly string _connectionString;

        public EmployeeRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetValue<string>("mydbkey");
        }

        public  IList<Employee> GetEmployeesAsync()
        {
            var employees = new List<Employee>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("SELECT EmployeeId, EmployeeName, EmployeeDesignation FROM Employee", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var employee = new Employee
                            {
                                EmployeeId = reader.GetInt32(0),
                                EmployeeName = reader.GetString(1),
                                EmployeeDesignation = reader.GetString(2)
                            };
                            employees.Add(employee);
                        }
                    }
                }
            }

            return employees;
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            Employee employee = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("SELECT EmployeeId, EmployeeName, EmployeeDesignation FROM Employees WHERE EmployeeId = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            employee = new Employee
                            {
                                EmployeeId = reader.GetInt32(0),
                                EmployeeName = reader.GetString(1),
                                EmployeeDesignation = reader.GetString(2)
                            };
                        }
                    }
                }
            }

            return employee;
        }

        // Add more methods for Create, Update, Delete as needed
    }
}
