using NetCore_UnitTestModel;

namespace NetCore_UnitTestBusiness.Interfaces
{
    public interface IBSalary
    {
        Salary ComputeSalary(decimal deminimis, int employeeId, int currentMonth, int numberOfAbsent, int numberOfUnderTimeHours, int numberOfLateHours);
        decimal DailyRate(decimal salaryRate, int currentMonth);
        decimal TaxRate(int salaryPeriodId, decimal basicPay, decimal totalDeductionWithoutTax);
    }
}
