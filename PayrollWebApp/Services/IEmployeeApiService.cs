using PayrollWebApp.Models;

namespace PayrollWebApp.Services
{
    public interface IEmployeeApiService
    {
        Task<List<EmployeeViewModel>> GetAllEmployeeAsync();
        Task<EmployeeViewModel?> CreateEmployeeAsync(EmployeeViewModel employee);
        Task<EmployeeViewModel?> GetEmployeeByIdAsync(int id);
        Task<EmployeeViewModel?> UpdateEmployeeAsync(EmployeeViewModel employee);
        Task<bool> DeleteEmployeeAsync(int id);
    }
}
