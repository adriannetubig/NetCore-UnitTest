using NetCore_UnitTestBusiness.Interfaces;
using NetCore_UnitTestData.Interfaces;
using NetCore_UnitTestModel;
using System;

namespace NetCore_UnitTestBusiness.Services
{
    public class BSalary : IBSalary
    {
        private readonly IDCalendar _iDCalendar;
        private readonly IDSalaryRate _iDSalaryRate;
        private readonly IDTaxRate _iDTaxRate;
        public BSalary(IDCalendar iDCalendar, IDSalaryRate iDSalaryRate, IDTaxRate iDTaxRate)
        {
            _iDCalendar = iDCalendar;
            _iDSalaryRate = iDSalaryRate;
            _iDTaxRate = iDTaxRate;
        }
        public Salary ComputeSalary (decimal deminimis, int employeeId, int currentMonth, int numberOfAbsent, int numberOfUnderTimeHours, int numberOfLateHours)
        {
            var salary = new Salary
            {
                NumberOfAbsent = numberOfAbsent,
                NumberOfUnderTimeHours = numberOfUnderTimeHours,
                NumberOfLateHours = numberOfLateHours,
                Deminimis = deminimis,
                HdmfDeduction = 50,
                PhilHealthDeduction = 275,
                SssDeduction = 290
            };
            salary.SalaryRate = _iDSalaryRate.Read(employeeId).Amount;

            salary.DailyRate = DailyRate(salary.SalaryRate, currentMonth);

            salary.WTaxDeduction = TaxRate(3, salary.BasicPay, salary.TotalDeductionWithoutTax);

            return salary;
        }

        public decimal DailyRate(decimal salaryRate, int currentMonth)
        {
            var workingDays = _iDCalendar.NumberOfWorkingDays(currentMonth);
            var dailyRate = salaryRate / workingDays;
            return Math.Round(dailyRate, 2, MidpointRounding.ToEven);
        }

        public decimal TaxRate(int salaryPeriodId, decimal basicPay, decimal totalDeductionWithoutTax)
        {
            var basicPayBeforeTax = basicPay - totalDeductionWithoutTax;

            var taxRate = _iDTaxRate.HighestTaxRate(salaryPeriodId, basicPayBeforeTax);

            if (taxRate == null)
                return 0;

            var taxableSalary = basicPayBeforeTax - taxRate.TaxAmount;
            var taxAmount = (taxRate.TaxPercentage / 100) * taxableSalary;
            var totalTax = taxRate.TaxAmount + taxAmount;
            return Math.Round(totalTax, 2, MidpointRounding.ToEven);
        }
    }
}
