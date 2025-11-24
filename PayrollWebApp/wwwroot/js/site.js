

// Variables

// Event Listeners
document.addEventListener('DOMContentLoaded', () => {

    const startField = document.querySelector("#DateStart");
    const endField = document.querySelector("#DateEnd");
    const patternField = document.querySelectorAll('input[name="WorkingDaysPattern"]');

    if (startField)
        startField.addEventListener("change", computeWorkingDays);

    if (endField)
        endField.addEventListener("change", computeWorkingDays);

    patternField.forEach(e => e.addEventListener("change", computeWorkingDays));

    computeWorkingDays();
});

async function getEmployeeById() {
    try {
        const id = getEmployeeId();
        const URL = EMPLOYEE_API_URL;
        const response = await fetch(`${URL}/${id}`);
        const employee = await response.json();

        console.log(employee);

        if (employee != null) {
            document.querySelector("h2").innerHTML = `${employee.firstName} ${employee.lastName}'s Payslips`;
        }
    } catch (error) {
        console.log('Unable to fetch employee.', error);
    }
}

function getEmployeeId() {
    const urlParts = window.location.pathname.split(`/`);
    return urlParts[urlParts.length - 1];
}

function parseNumber(value) {
    return parseFloat(value.replace(/,/g, '')) || 0;
}

async function computeTakeHomePay() {
    const dailyRate = parseNumber(document.querySelector("#FormattedDailyRate").value);
    const numberOfWorkingDays = parseNumber(document.querySelector("#ActualWorkingDays").value);
    const dateOfBirth = new Date(document.querySelector("#DateOfBirth").value);
    const startDate = new Date(new Date(document.querySelector("#DateStart").value).toDateString());
    const endDate = new Date(new Date(document.querySelector("#DateEnd").value).toDateString());

    let gross = dailyRate * (numberOfWorkingDays * 2);

    const dobMonth = dateOfBirth.getMonth();
    const dobDay = dateOfBirth.getDate();
    const dobThisYear = new Date(new Date(startDate.getFullYear(), dobMonth, dobDay).toDateString());

    // If date of birth is inside start and end date
    if (dobThisYear >= startDate && dobThisYear <= endDate) {
        gross += dailyRate;
    }

    document.querySelector("#NetPay").value = formatNumber(gross);
}

async function computeWorkingDays() {

    const startPeriod = new Date(document.querySelector("#DateStart").value);
    const endPeriod = new Date(document.querySelector("#DateEnd").value);
    const workingDaysPattern = document.querySelector('input[name="WorkingDaysPattern"]:checked')?.value;

    if (!startPeriod || !endPeriod || !workingDaysPattern)
        return;

    const mapOfDays = {
        'M': 1,
        'T': 2,
        'W': 3,
        'H': 4,
        'F': 5,
        'S': 6
    };

    let daysWorked = 0;

    for (let startDate = new Date(startPeriod); startDate <= endPeriod; startDate.setDate(startDate.getDate() + 1)) {
        for (let char of workingDaysPattern) {
            if (startDate.getDay() === mapOfDays[char]) {
                daysWorked++;
                break;
            }
        }
    }

    document.querySelector("#ActualWorkingDays").value = daysWorked;
    computeTakeHomePay();
}

function formatNumber(num) {
    return num.toLocaleString("en-us", {
        minimumFractionDigits: 2,
        maximumFractionDigits: 2
    });
}

setTimeout(() => {
    const alerts = document.querySelectorAll('.alert');
    alerts.forEach(alert => alert.remove());
}, 5000); // 5 seconds