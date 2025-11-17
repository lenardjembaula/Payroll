using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;

namespace PayrollWebApp.Models
{
    public class PayslipViewModel
    {
        public int Id { get; set; }
        public DateTime DateStart { get; set; }
        public string FormattedDateStart => DateStart.ToString("MMM dd, yyyy");
        public DateTime DateEnd { get; set; }
        public string FormattedDateEnd => DateEnd.ToString("MMM dd, yyyy");
        public string? WorkingDaysPattern { get; set; }
        public int ActualWorkingDays { get; set; }
        public float? NetPay { get; set; }
        public string FormattedNetPay => 
            (NetPay ?? 0).ToString("#,###.00");


        // Navigation Property
        [JsonIgnore]
        [BindNever]
        public EmployeeViewModel? Employee { get; set; }
        public int EmployeeId { get; set; } // Foreign Key


        // For Frontend and computation
        public string? EmployeeName { get; set; } = default!;
        public float? DailyRate { get; set; }
        public string FormattedDailyRate =>DailyRate.HasValue ? DailyRate.Value.ToString("#,###0.00") : string.Empty;
        public DateTime? DateOfBirth { get; set; }
        public string FormattedDateOfBirth => DateOfBirth.HasValue ? DateOfBirth.Value.ToString("yyyy-MM-dd") : string.Empty;

    }
}
