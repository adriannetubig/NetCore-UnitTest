using NetCore_UnitTestData.Interfaces;
using NetCore_UnitTestEntity;
using System.Collections.Generic;
using System.Linq;

namespace NetCore_UnitTestData.Services
{
    public class DTaxRate : IDTaxRate
    {
        private readonly List<ETaxRate> _eTaxRates;

        public DTaxRate()
        {
            _eTaxRates = new List<ETaxRate>()
            {
                new ETaxRate
                {
                    TaxRateId = 10,
                    SalaryPeriodId = 3,
                    CompensationLevel = 10417,
                    TaxAmount = 0,
                    TaxPercentage = 20
                },
                new ETaxRate
                {
                    TaxRateId = 11,
                    SalaryPeriodId = 3,
                    CompensationLevel = 16667,
                    TaxAmount = 1250,
                    TaxPercentage = 0
                },
                new ETaxRate
                {
                    TaxRateId = 12,
                    SalaryPeriodId = 3,
                    CompensationLevel = 33333,
                    TaxAmount = 5416.67m,
                    TaxPercentage = 30
                },
                new ETaxRate
                {
                    TaxRateId = 13,
                    SalaryPeriodId = 3,
                    CompensationLevel = 83333,
                    TaxAmount = 20416.67m,
                    TaxPercentage = 32
                },
                new ETaxRate
                {
                    TaxRateId = 14,
                    SalaryPeriodId = 3,
                    CompensationLevel = 333333,
                    TaxAmount = 100416.67m,
                    TaxPercentage = 35
                },
            };
        }

        public ETaxRate HighestTaxRate(int salaryPeriodId, decimal salaryRate)
        {
            return _eTaxRates.OrderBy(a => a.CompensationLevel).FirstOrDefault(a => a.SalaryPeriodId == salaryPeriodId && a.CompensationLevel <= salaryRate);
        }

    }
}
