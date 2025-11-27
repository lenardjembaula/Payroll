namespace PayrollAPI.Services
{
    public class PayrollHelperService
    {
        private static Random _random = new Random();

        public int ComputeActualWorkingDays(DateTime dateStart, DateTime dateEnd, string workingDaysPattern)
        {
            int DaysWorked = 0;
            var MapOfDays = new Dictionary<char, DayOfWeek>
            {
                { 'M', DayOfWeek.Monday },
                { 'T', DayOfWeek.Tuesday },
                { 'W', DayOfWeek.Wednesday },
                { 'H', DayOfWeek.Thursday },
                { 'F', DayOfWeek.Friday },
                { 'S', DayOfWeek.Saturday }
            };

            for (var date = dateStart; date <= dateEnd; date = date.AddDays(1))
            {
                foreach (var dayChar in workingDaysPattern)
                {
                    if (MapOfDays[dayChar] == date.DayOfWeek)
                    {
                        DaysWorked++;
                        break;
                    }
                }
            }

            return DaysWorked;
        }

        // Will use this function if we have to select the dates that the employee is present
        public List<DateTime> GetWorkingDates(DateTime dateStart, DateTime dateEnd, string workingDaysPattern)
        {
            var workingDates = new List<DateTime>();

            var MapOfDays = new Dictionary<char, DayOfWeek>
            {
                { 'M', DayOfWeek.Monday },
                { 'T', DayOfWeek.Tuesday },
                { 'W', DayOfWeek.Wednesday },
                { 'H', DayOfWeek.Thursday },
                { 'F', DayOfWeek.Friday },
                { 'S', DayOfWeek.Saturday }
            };

            for (var date = dateStart; date <= dateEnd; date = date.AddDays(1))
            {
                foreach (var dayChar in workingDaysPattern)
                {
                    if (MapOfDays.ContainsKey(dayChar) && MapOfDays[dayChar] == date.DayOfWeek)
                    {
                        workingDates.Add(date);
                        break;
                    }
                }
            }

            return workingDates;
        }

        public string GenerateEmployeeCode(string lastName, DateTime dob)
        {
            // First Part BAU-
            string firstPart = lastName.Length >= 3
                ? lastName.Substring(0, 3).ToUpper()
                : (lastName.ToUpper() + "*".PadRight(3 - lastName.Length, '*'));

            // Second Part -00000 Random 5 numbers
            int rand = _random.Next(0, 100000); // from 0 up to 99999, 100k is not included
            string middlePart = rand.ToString("D5"); // means convert the random number to 5 digit number eg. rand number = 293, it will become 00293

            // Last Part -14JUL1997
            string lastPart = dob.ToString("ddMMMyyyy").ToUpper();

            // Complete Employee Code
            string returnThis = $"{firstPart}-{middlePart}-{lastPart}";

            return returnThis;

        }

        public float ComputeTakeHomePay(int actualWorkingDays, bool isDoB, float dailyRate)
        {
            float takeHomePay = (dailyRate * 2) * actualWorkingDays;

            if (isDoB)
                takeHomePay += dailyRate;

            return takeHomePay;
            
        }
    }
}
