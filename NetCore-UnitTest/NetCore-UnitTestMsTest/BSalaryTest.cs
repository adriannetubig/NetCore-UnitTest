using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NetCore_UnitTestBusiness.Interfaces;
using NetCore_UnitTestBusiness.Services;
using NetCore_UnitTestData.Interfaces;
using NetCore_UnitTestData.Services;
using NetCore_UnitTestModel;

namespace NetCore_UnitTestMsTest
{
    [TestClass]
    public class BSalaryTest
    {
        [TestMethod]
        public void TestMethod1()
        {
        }

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
        [TestMethod]
        public void ComputeDailyRate()
        {
            var dailyRate = _iBSalary.DailyRate(15000, 2);
            Assert.AreEqual(535.71m, dailyRate);
        }

        [TestMethod]
        public void ComputeTax()
        {
            var TaxRate = _iBSalary.TaxRate(3, 10417, 0);
            Assert.AreEqual(2083.4m, TaxRate);
        }

        [TestMethod]
        public void ComputeSalary()
        {
            var salary = _iBSalary.ComputeSalary(0, 1, 2, 0, 0, 0);

            Assert.AreEqual(15000, salary.SalaryRate);
            Assert.AreEqual(535.71m, salary.DailyRate);
            Assert.AreEqual(0, salary.NumberOfAbsent);
            Assert.AreEqual(0, salary.AbsentDeduction);
            Assert.AreEqual(0, salary.NumberOfUnderTimeHours);
            Assert.AreEqual(0, salary.UnderTimeDeduction);
            Assert.AreEqual(0, salary.NumberOfLateHours);
            Assert.AreEqual(0, salary.LateDeduction);
            Assert.AreEqual(15000, salary.BasicPay);
            Assert.AreEqual(0, salary.Deminimis);
            Assert.AreEqual(0, salary.OvertimePay);
            Assert.AreEqual(0, salary.TotalAddition);
            Assert.AreEqual(0, salary.SssSalaryLoanDeduction);
            Assert.AreEqual(50, salary.HdmfDeduction);
            Assert.AreEqual(275, salary.PhilHealthDeduction);
            Assert.AreEqual(290, salary.SssDeduction);
            Assert.AreEqual(615, salary.TotalDeductionWithoutTax);
            Assert.AreEqual(2877, salary.WTaxDeduction);
            Assert.AreEqual(3492, salary.TotalDeduction);
            Assert.AreEqual(11508, salary.NetPay);
        }

        //With Moq
        [TestMethod]
        public void ComputeDailyRateWithMoq()
        {
            _moqDCalendar.Setup(a => a.NumberOfWorkingDays(It.IsAny<int>())).Returns(10);
            ReinitiateMoqValues();

            var dailyRate = _iBSalary.DailyRate(15000, 2);
            Assert.AreEqual(1500, dailyRate);
        }

        //ParameterizedTest
        [DataTestMethod]
        [DataRow(0)]
        [DataRow(100)]
        public void Deminimiss(int deminimis)
        {
            var salary = new Salary
            {
                Deminimis = deminimis
            };
            Assert.AreEqual(deminimis, salary.Deminimis);
            Assert.AreEqual(deminimis, salary.TotalDeductionWithoutTax);
            Assert.AreEqual(deminimis, salary.TotalDeduction);
            Assert.AreEqual(0, salary.NetPay);
        }
    }
}
