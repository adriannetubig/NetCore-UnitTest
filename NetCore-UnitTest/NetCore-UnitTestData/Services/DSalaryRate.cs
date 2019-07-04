using NetCore_UnitTestData.Interfaces;
using NetCore_UnitTestEntity;
using System.Collections.Generic;
using System.Linq;

namespace NetCore_UnitTestData.Services
{
    public class DSalaryRate: IDSalaryRate
    {
        private readonly List<ESalaryRate> _eSalaryRates;

        public DSalaryRate()
        {
            _eSalaryRates = new List<ESalaryRate>()
            {
                new ESalaryRate
                {
                    Amount = 15000,
                    SalaryRateId = 10,
                    EmployeeId = 1,
                }
            };
        }

        public ESalaryRate Read(int employeeId)
        {
            return _eSalaryRates.FirstOrDefault(a => a.EmployeeId == employeeId);
        }

    }
}
