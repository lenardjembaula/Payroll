using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayrollAPI.Data;
using PayrollAPI.Entities;
using PayrollAPI.Services;

namespace PayrollAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly PayrollHelperService _helper;

        public EmployeesController(ApplicationDbContext context, PayrollHelperService helper)
        {
            _context = context;
            _helper = helper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Employees.ToList());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Employee employee)
        {
            employee.EmployeeCode = _helper.GenerateEmployeeCode(employee.LastName, employee.DoB);
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return Ok(employee);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Employee employee)
        {
            var editEmployee = await _context.Employees.FindAsync(id);

            if (editEmployee == null)
                return NotFound();

            editEmployee.FirstName = employee.FirstName;
            editEmployee.LastName = employee.LastName;
            editEmployee.DailyRate = employee.DailyRate;
            editEmployee.DoB = employee.DoB;
            editEmployee.EmployeeCode = _helper.GenerateEmployeeCode(employee.LastName, employee.DoB);

            await _context.SaveChangesAsync();
            return Ok(editEmployee);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteEmployee = await _context.Employees.FindAsync(id);

            if (deleteEmployee == null)
                return NotFound();

            _context.Employees.Remove(deleteEmployee);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
