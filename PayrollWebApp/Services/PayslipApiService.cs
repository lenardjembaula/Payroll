using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PayrollWebApp.Data;
using PayrollWebApp.Models;

namespace PayrollWebApp.Services
{
    public class PayslipApiService : IPayslipApiService
    {
        private readonly HttpClient _httpClient;
        public PayslipApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Get All Payslip of Employee
        public async Task<List<PayslipViewModel>> GetAllPayslipByEmployeeIdAsync(int employeeId)
        {
            var employeePayslips =  await _httpClient.GetFromJsonAsync<List<PayslipViewModel>>($"{Const.PAYSLIPS_ENDPOINT}/employee/{employeeId}");

            return employeePayslips ?? new List<PayslipViewModel>();
        }

        // Create Payslip
        public async Task<PayslipViewModel?> CreatePayslipAsync(PayslipViewModel payslip)
        {
            var response = await _httpClient.PostAsJsonAsync(Const.PAYSLIPS_ENDPOINT, payslip);

            return response.IsSuccessStatusCode
                ? await response.Content.ReadFromJsonAsync<PayslipViewModel>()
                : null;
        }

        // Get Payslip by Id
        public async Task<PayslipViewModel?> GetPayslipByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<PayslipViewModel>($"{Const.PAYSLIPS_ENDPOINT}/{id}");
        }

        // Edit Payslips
        public async Task<PayslipViewModel?> EditPayslipAsync(PayslipViewModel payslip)
        {
            var response = await _httpClient.PutAsJsonAsync($"{Const.PAYSLIPS_ENDPOINT}/{payslip.Id}", payslip);

            return response.IsSuccessStatusCode
                ? await response.Content.ReadFromJsonAsync<PayslipViewModel>()
                : null;
        }

        public async Task<bool> DeletePayslipAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{Const.PAYSLIPS_ENDPOINT}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
