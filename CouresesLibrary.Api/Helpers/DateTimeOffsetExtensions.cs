using System;

namespace CouresesLibrary.Api.Helpers
{
    public static class DateTimeOffsetExtensions
    {

        public static int GetAge(this DateTimeOffset dateTimeOffset)
        {

            var currentDateTime = DateTimeOffset.UtcNow;
            var age = currentDateTime.Year - dateTimeOffset.Year;
            if (currentDateTime < dateTimeOffset.AddYears(age))
            {
                age--;
            }

            return age;
        }
    }
}
