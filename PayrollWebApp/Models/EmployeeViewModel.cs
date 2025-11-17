using System.Text.Json.Serialization;

namespace PayrollWebApp.Models
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        public string? EmployeeCode { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public DateTime DoB { get; set; }
        public string FormattedDoB => DoB.ToString("MMM dd, yyyy");

        public float DailyRate { get; set; }
        public string FormattedDailyRate => DailyRate.ToString("#,###.00");

        // Navigation Property
        [JsonIgnore]
        public List<PayslipViewModel>? Payslips { get; set; }
    }
}
