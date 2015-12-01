using System;
using System.Globalization;
using System.Windows;

namespace DiamondApp.Tools
{
    public static class DateTimeConverter
    {
        public static string ToMonthEnglishName(DateTime? dateTime)
        {
            DateTime dt = DateTime.MinValue;
            if (dateTime.HasValue)
            {
                dt = dateTime.Value;
            }
            CultureInfo ci = new CultureInfo("en-GB");
            var retStr = dt != DateTime.MinValue ? dt.ToString("MMMM", ci) : String.Empty;
            return retStr;
        }

        public static string ToMonthPolishName(DateTime? dateTime)
        {
            DateTime dt = DateTime.MinValue;
            if (dateTime.HasValue)
            {
                dt = dateTime.Value;
            }
            var retStr = dt != DateTime.MinValue ? CultureInfo.CurrentUICulture.DateTimeFormat.GetMonthName(dt.Month) : String.Empty;
            return retStr;
        }

        public static int ToIntMonth(DateTime? dateTime)
        {
            DateTime dt = DateTime.MinValue;
            if (dateTime.HasValue)
            {
                dt = dateTime.Value;
            }
            var retStr = dt != DateTime.MinValue ? dt.Month : 13;
            return retStr;
        }
    }
}
