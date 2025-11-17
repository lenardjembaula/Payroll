namespace PayrollWebApp.Models
{
    public class PayslipDto
    {
        public int Id { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public string WorkingDaysPattern { get; set; }
        public int ActualWorkingDays { get; set; }
        public float NetPay { get; set; }
        public int EmployeeId { get; set; }
    }
}
