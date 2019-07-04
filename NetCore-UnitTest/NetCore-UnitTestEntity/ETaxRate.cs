using System;

namespace NetCore_UnitTestEntity
{
    public class ETaxRate
    {
        public decimal CompensationLevel { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TaxPercentage { get; set; }
        public int SalaryPeriodId { get; set; }
        public int TaxRateId { get; set; }
    }
}
