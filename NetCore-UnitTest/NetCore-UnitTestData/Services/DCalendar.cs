using NetCore_UnitTestData.Interfaces;
using System;

namespace NetCore_UnitTestData.Services
{
    public class DCalendar : IDCalendar
    {
        public int NumberOfWorkingDays (int month)
        {
            switch (month)
            {
                case 2: return 28;
                case 4:
                case 6:
                case 9:
                case 11: return 31;
                default: return 31;
            }
        }
    }
}
