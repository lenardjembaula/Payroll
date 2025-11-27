using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;

namespace PayrollAPI.Entities
{
    public class Payslip
    {
        public int Id { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public string WorkingDaysPattern { get; set; } = default!;
        public int ActualWorkingDays { get; set; }

        public float NetPay { get; set; }

        // Navigation Property
        [JsonIgnore]
        public Employee? Employee { get; set; }
        public int EmployeeId { get; set; } // Foreign Key


    }
}
