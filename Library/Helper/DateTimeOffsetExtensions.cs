using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Helper
{
    public static class DateTimeOffsetExtensions
    {
        public static int GetCurrentAge(DateTimeOffset dateTimeOffset)
        {
            var currentDate = DateTime.UtcNow;
            int age = currentDate.Year - dateTimeOffset.Year;
            if(currentDate < dateTimeOffset.AddYears(age))
            {
                age--;
            }
            return age;
        }
    }
}
