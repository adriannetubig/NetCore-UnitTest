using NetCore_UnitTestEntity;

namespace NetCore_UnitTestData.Interfaces
{
    public interface IDTaxRate
    {
        ETaxRate HighestTaxRate(int salaryPeriodId, decimal salaryRate);
    }
}
