using System;

namespace NetCore_UnitTestModel
{
    public class Salary
    {
        public decimal SalaryRate { get; set; }
        public decimal DailyRate { get; set; }
        public int NumberOfAbsent { get; set; }
        public decimal AbsentDeduction => DailyRate * NumberOfAbsent;
        public int NumberOfUnderTimeHours { get; set; }
        public decimal UnderTimeDeduction => (DailyRate / 8) * NumberOfUnderTimeHours;
        public int NumberOfLateHours { get; set; }
        public decimal LateDeduction => (DailyRate / 8) * NumberOfLateHours;
        public decimal BasicPay => SalaryRate - AbsentDeduction - UnderTimeDeduction - LateDeduction;
        public decimal Deminimis { get; set; }
        public decimal OvertimePay { get; set; }
        public decimal TotalAddition => Deminimis + OvertimePay;
        public decimal SssSalaryLoanDeduction { get; set; }
        public decimal HdmfDeduction { get; set; }
        public decimal PhilHealthDeduction { get; set; }
        public decimal SssDeduction { get; set; }
        public decimal TotalDeductionWithoutTax => Deminimis + SssSalaryLoanDeduction + HdmfDeduction + PhilHealthDeduction + SssDeduction;
        public decimal WTaxDeduction { get; set; }
        public decimal TotalDeduction => TotalDeductionWithoutTax + WTaxDeduction;
        public decimal NetPay => BasicPay + TotalAddition - TotalDeduction;
    }
}
