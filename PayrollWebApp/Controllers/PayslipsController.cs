using Microsoft.AspNetCore.Mvc;
using PayrollWebApp.Models;
using PayrollWebApp.Services;
using System.Threading.Tasks;

namespace PayrollWebApp.Controllers
{
    public class PayslipsController : Controller
    {

        private readonly IPayslipApiService _payslipApiService;
        private readonly IEmployeeApiService _employeeApiService;

        public PayslipsController(IPayslipApiService payslipApiService, IEmployeeApiService employeeApiService)
        {
            _payslipApiService = payslipApiService;
            _employeeApiService = employeeApiService;
        }

        [Route("Payslips/Employees/{employeeId}")]
        public async Task<IActionResult> Index(int employeeId)
        {
            var employeePayslips = await _payslipApiService.GetAllPayslipByEmployeeIdAsync(employeeId);
            ViewBag.EmployeeId = employeeId;
            return View(employeePayslips);
        }

        [HttpGet]
        [Route("Payslips/Employees/{employeeId}/Create")]
        public async Task<IActionResult> Create(int employeeId)
        {
            var employee = await _employeeApiService.GetEmployeeByIdAsync(employeeId);
            var payslip = new PayslipViewModel
            {
                EmployeeId = employeeId,
                DateStart = DateTime.Today,
                DateEnd = DateTime.Today,
                EmployeeName = $"{employee.FirstName} {employee.LastName}",
                DailyRate = employee.DailyRate,
                DateOfBirth = employee.DoB
            };

            ViewBag.EmployeeId = employeeId;
            return View(payslip);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Payslips/Employees/{employeeId}/Create")]
        public async Task<IActionResult> Create(PayslipViewModel payslip)
        {
            if (ModelState.IsValid)
            {
                var newPayslip = await _payslipApiService.CreatePayslipAsync(payslip);
                if (newPayslip != null)
                {
                    TempData["PayslipPageSuccess"] = "Payslip successfully Created.";
                    return RedirectToAction(nameof(Index), new { employeeId = payslip.EmployeeId });
                }
            }

            TempData["PayslipPageError"] = "Failed to create payslip.";
            return View(payslip);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            PayslipViewModel payslip = await _payslipApiService.GetPayslipByIdAsync(id);
            if (payslip == null)
                return NotFound();

            var employee = await _employeeApiService.GetEmployeeByIdAsync(payslip.EmployeeId);
            payslip.EmployeeName = $"{employee.FirstName} {employee.LastName}";
            payslip.DailyRate = employee.DailyRate;
            payslip.EmployeeId = employee.Id;
            payslip.DateOfBirth = employee.DoB;

            return View(payslip);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PayslipViewModel payslip)
        {
            if (!ModelState.IsValid)
                return View(payslip);

            var updatedPayslip = await _payslipApiService.EditPayslipAsync(payslip);
            if (updatedPayslip != null)
            {
                TempData["PayslipPageSuccess"] = "Payslip successfully Updated.";
                return RedirectToAction(nameof(Index), new { employeeId = payslip.EmployeeId });
            }

            TempData["PayslipPageError"] = "Failed to update payslip.";
            return View(payslip);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, int employeeId)
        {
            bool result = await _payslipApiService.DeletePayslipAsync(id);
            if (result)
                TempData["PayslipPageSuccess"] = "Payslip successfully deleted.";
            else
                TempData["PayslipPageError"] = "Failed to delete payslip.";

            return RedirectToAction(nameof(Index), new { employeeId = employeeId });
        }
    }
}
