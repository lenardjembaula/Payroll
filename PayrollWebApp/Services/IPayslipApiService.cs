using PayrollWebApp.Models;

namespace PayrollWebApp.Services
{
    public interface IPayslipApiService
    {
        Task<List<PayslipViewModel>> GetAllPayslipByEmployeeIdAsync(int employeeId);
        Task<PayslipViewModel?> CreatePayslipAsync(PayslipViewModel payslip);
        Task<PayslipViewModel?> GetPayslipByIdAsync(int id);
        Task<PayslipViewModel?> EditPayslipAsync(PayslipViewModel payslip);
        Task<bool> DeletePayslipAsync(int id);

    }
}
