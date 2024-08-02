using Microsoft.AspNetCore.Mvc;
using MVCApp.Models;
using System.Diagnostics;

namespace MVCApp.Controllers
{
    public class HomeController : Controller
    {
        // In-memory list of employees for demonstration
        //private static List<Employee> employees = new List<Employee>
        //{
        //    new Employee { EmployeeId = 1, EmployeeName = "John Doe", EmployeeDesignation = "Software Developer" },
        //    new Employee { EmployeeId = 2, EmployeeName = "Jane Smith", EmployeeDesignation = "Project Manager" }
        //};

        private readonly ILogger<HomeController> _logger;
        public readonly EmployeeRepository EmployeeRepository;
        public HomeController(ILogger<HomeController> logger, EmployeeRepository employeeRepository)
        {
            _logger = logger;
            
            EmployeeRepository = employeeRepository;
        }

        public IActionResult Index()
        {
            return View(EmployeeRepository.GetEmployeesAsync());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
