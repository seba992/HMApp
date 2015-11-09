using System;
using System.Globalization;

namespace DiamondApp.Tools
{
    public static class DateTimeConverter
    {
        public static string ToMonthName(this DateTime dateTime)
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(dateTime.Month);
        }
    }
}
