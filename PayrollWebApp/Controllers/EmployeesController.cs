using Microsoft.AspNetCore.Mvc;
using PayrollWebApp.Models;
using PayrollWebApp.Data;
using PayrollWebApp.Services;

namespace PayrollWebApp.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly EmployeeApiService _employeeApiService;

        public EmployeesController(EmployeeApiService apiService)
        {
            _employeeApiService = apiService;
        }


        public async Task<IActionResult> Index()
        {
            var employees = await _employeeApiService.GetAllEmployeeAsync();
            return View(employees);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeViewModel employee)
        {
            if (!ModelState.IsValid)
                return View(employee);

            var newEmployee = await _employeeApiService.CreateEmployeeAsync(employee);
            if (newEmployee != null)
            {
                TempData["EmployeePageSuccess"] = "Employee successfully Created.";
                return RedirectToAction(nameof(Index));
            }

            TempData["EmployeePageError"] = "Failed to create employee.";
            return View(employee);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _employeeApiService.GetEmployeeByIdAsync(id);
            if (employee == null)
                return NotFound();

            return View(employee);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EmployeeViewModel employee)
        {
            if (!ModelState.IsValid)
                return View(employee);

            var updateEmployee = await _employeeApiService.UpdateEmployeeAsync(employee);
            if (updateEmployee != null)
            {
                TempData["EmployeePageSuccess"] = "Employee successfully Updated.";
                return RedirectToAction(nameof(Index));
            }

            TempData["EmployeePageError"] = "Failed to update employee.";
            return View(employee);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await _employeeApiService.DeleteEmployeeAsync(id);
            if (isDeleted)
                TempData["EmployeePageSuccess"] = "Employee successfully Deleted.";
            else
                TempData["EmployeePageError"] = "Failed to delete employee.";

            return RedirectToAction(nameof(Index));
        }
    }
}
