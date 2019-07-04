using Moq;
using NetCore_UnitTestBusiness.Interfaces;
using NetCore_UnitTestBusiness.Services;
using NetCore_UnitTestData.Interfaces;
using NetCore_UnitTestData.Services;
using NetCore_UnitTestModel;
using Xunit;

namespace NetCore_UnitTestXUnit
{
    public class BSalaryTest
    {
        private readonly IDCalendar _iDCalendar;
        private readonly IDSalaryRate _iDSalaryRate;
        private readonly IDTaxRate _iDTaxRate;
        private IBSalary _iBSalary;

        Mock<IDCalendar> _moqDCalendar;
        Mock<IDSalaryRate> _moqDSalaryRate;
        Mock<IDTaxRate> _moqDTaxRate;

        public BSalaryTest()
        {
            _iDCalendar = new DCalendar();
            _iDSalaryRate = new DSalaryRate();
            _iDTaxRate = new DTaxRate();

            _moqDCalendar = new Mock<IDCalendar>();
            _moqDSalaryRate = new Mock<IDSalaryRate>();
            _moqDTaxRate = new Mock<IDTaxRate>();

            _iBSalary = new BSalary(_iDCalendar, _iDSalaryRate, _iDTaxRate);
        }

        private void ReinitiateMoqValues()
        {
            _iBSalary = new BSalary(_moqDCalendar.Object, _moqDSalaryRate.Object, _moqDTaxRate.Object);
        }

        //Without Moq
        [Fact]
        public void ComputeDailyRate()
        {
            var dailyRate = _iBSalary.DailyRate(15000, 2);
            Assert.Equal(535.71m, dailyRate);
        }

        [Fact]
        public void ComputeTax()
        {
            var TaxRate = _iBSalary.TaxRate(3, 10417, 0);
            Assert.Equal(2083.4m, TaxRate);
        }

        [Fact]
        public void ComputeSalary()
        {
            var salary = _iBSalary.ComputeSalary(0, 1, 2, 0, 0, 0);

            Assert.Equal(15000, salary.SalaryRate);
            Assert.Equal(535.71m, salary.DailyRate);
            Assert.Equal(0, salary.NumberOfAbsent);
            Assert.Equal(0, salary.AbsentDeduction);
            Assert.Equal(0, salary.NumberOfUnderTimeHours);
            Assert.Equal(0, salary.UnderTimeDeduction);
            Assert.Equal(0, salary.NumberOfLateHours);
            Assert.Equal(0, salary.LateDeduction);
            Assert.Equal(15000, salary.BasicPay);
            Assert.Equal(0, salary.Deminimis);
            Assert.Equal(0, salary.OvertimePay);
            Assert.Equal(0, salary.TotalAddition);
            Assert.Equal(0, salary.SssSalaryLoanDeduction);
            Assert.Equal(50, salary.HdmfDeduction);
            Assert.Equal(275, salary.PhilHealthDeduction);
            Assert.Equal(290, salary.SssDeduction);
            Assert.Equal(615, salary.TotalDeductionWithoutTax);
            Assert.Equal(2877, salary.WTaxDeduction);
            Assert.Equal(3492, salary.TotalDeduction);
            Assert.Equal(11508, salary.NetPay);
        }

        //With Moq
        [Fact]
        public void ComputeDailyRateWithMoq()
        {
            _moqDCalendar.Setup(a => a.NumberOfWorkingDays(It.IsAny<int>())).Returns(10);
            ReinitiateMoqValues();

            var dailyRate = _iBSalary.DailyRate(15000, 2);
            Assert.Equal(1500, dailyRate);
        }

        //ParameterizedTest
        [Theory]
        [InlineData(0)]
        [InlineData(100)]
        public void Deminimiss(decimal deminimis)
        {
            var salary = new Salary
            {
                Deminimis = deminimis
            };
            Assert.Equal(deminimis, salary.Deminimis);
            Assert.Equal(deminimis, salary.TotalDeductionWithoutTax);
            Assert.Equal(deminimis, salary.TotalDeduction);
            Assert.Equal(0, salary.NetPay);
        }
    }
}
