using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PayrollWebApp.Models
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        public string? EmployeeCode { get; set; }


        [Required(ErrorMessage = "First name is required.")]
        public string? FirstName { get; set; }


        [Required(ErrorMessage = "Last name is required.")]
        public string? LastName { get; set; }


        [Required(ErrorMessage = "Date of Birth is missing.")]
        public DateTime? DoB { get; set; }
        public string FormattedDoB => DoB.HasValue ? DoB.Value.ToString("MMM dd, yyyy") : string.Empty;


        [Required(ErrorMessage = "Daily Rate is required.")]
        public float? DailyRate { get; set; }
        public string FormattedDailyRate => DailyRate.HasValue ? DailyRate.Value.ToString("#,###.00") : string.Empty;


        // Navigation Property
        [JsonIgnore]
        public List<PayslipViewModel>? Payslips { get; set; }
    }
}
