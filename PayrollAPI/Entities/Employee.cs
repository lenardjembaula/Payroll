using System.Text.Json.Serialization;

namespace PayrollAPI.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string? EmployeeCode { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime DoB { get; set; }
        public float DailyRate { get; set; }

        // Navigation Property
        [JsonIgnore]
        public List<Payslip>? Payslips { get; set; }
    }
}
