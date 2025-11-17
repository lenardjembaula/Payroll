using PayrollWebApp.Models;
using PayrollWebApp.Data;

namespace PayrollWebApp.Services
{
    // This is the class for consuming the API/Employees
    public class EmployeeApiService
    {
        private readonly HttpClient _httpClient;

        public EmployeeApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Get All
        public async Task<List<EmployeeViewModel>> GetAllEmployeeAsync()
        {
            var employees = await _httpClient.GetFromJsonAsync<List<EmployeeViewModel>>(Const.EMPLOYEES_ENDPOINT);

            return employees ?? new List<EmployeeViewModel>();

        }

        // Create
        public async Task<EmployeeViewModel?> CreateEmployeeAsync(EmployeeViewModel employee)
        {
            var response = await _httpClient.PostAsJsonAsync(Const.EMPLOYEES_ENDPOINT, employee);

            return response.IsSuccessStatusCode
                ? await response.Content.ReadFromJsonAsync<EmployeeViewModel>()
                : null;
        }

        // Get One Employee
        public async Task<EmployeeViewModel?> GetEmployeeByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<EmployeeViewModel>($"{Const.EMPLOYEES_ENDPOINT}/{id}");
        }

        // Update
        public async Task<EmployeeViewModel?> UpdateEmployeeAsync(EmployeeViewModel employee)
        {
            var response = await _httpClient.PutAsJsonAsync($"{Const.EMPLOYEES_ENDPOINT}/{employee.Id}", employee);

            return response.IsSuccessStatusCode 
                ? await response.Content.ReadFromJsonAsync<EmployeeViewModel>() 
                : null;
        }

        // Delete One Employee
        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{Const.EMPLOYEES_ENDPOINT}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
