using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayrollAPI.Data;
using PayrollAPI.Entities;
using PayrollAPI.Services;

namespace PayrollAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayslipsController : ControllerBase
    {
        private readonly PayrollHelperService _helper;
        private readonly ApplicationDbContext _context;

        public PayslipsController(PayrollHelperService helper, ApplicationDbContext context)
        {
            _helper = helper;
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var payslips = _context.Payslips.ToList();
            return Ok(payslips);
        }

        [HttpGet("employee/{employeeId}")]
        public IActionResult GetPayslipByEmployeeId(int employeeId)
        {
            var payslips = _context.Payslips
                .Where(p => p.EmployeeId == employeeId)
                .Select(p => new
                {
                    p.Id,
                    p.EmployeeId,
                    EmployeeName = p.Employee.FirstName + " " + p.Employee.LastName,
                    p.DateStart,
                    p.DateEnd,
                    p.WorkingDaysPattern,
                    p.ActualWorkingDays,
                    p.NetPay
                })
                .ToList();

            return Ok(payslips);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var payslip = await _context.Payslips.FindAsync(id);
            if (payslip == null) return NotFound();
            return Ok(payslip);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Payslip payslip)
        {
            // Kunin employee para sa DailyRate
            var employee = await _context.Employees.FindAsync(payslip.EmployeeId);
            if (employee == null) return NotFound();

            // Compute ActualWorkingDays
            payslip.ActualWorkingDays = _helper.ComputeActualWorkingDays(
                payslip.DateStart,
                payslip.DateEnd,
                payslip.WorkingDaysPattern
            );


            payslip.NetPay = _helper.ComputeTakeHomePay(payslip.ActualWorkingDays,
                                                        _helper.isDoB(employee.DoB, payslip.DateStart, payslip.DateEnd),
                                                        employee.DailyRate);

            _context.Payslips.Add(payslip);
            await _context.SaveChangesAsync();
            return Ok(payslip);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Payslip updatedPayslip)
        {
            var payslip = await _context.Payslips.FindAsync(id);
            if (payslip == null) 
                return NotFound();

            payslip.DateStart = updatedPayslip.DateStart;
            payslip.DateEnd = updatedPayslip.DateEnd;
            payslip.WorkingDaysPattern = updatedPayslip.WorkingDaysPattern;
            payslip.ActualWorkingDays = _helper.ComputeActualWorkingDays(
                payslip.DateStart,
                payslip.DateEnd,
                payslip.WorkingDaysPattern
            );

            payslip.NetPay = _helper.ComputeTakeHomePay(payslip.ActualWorkingDays,
                                                        _helper.isDoB(payslip.Employee.DoB, payslip.DateStart, payslip.DateEnd),
                                                        payslip.Employee.DailyRate);

            await _context.SaveChangesAsync();
            return Ok(payslip);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var payslip = await _context.Payslips.FindAsync(id);
            if (payslip == null) return NotFound();

            _context.Payslips.Remove(payslip);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
