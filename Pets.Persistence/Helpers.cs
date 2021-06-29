namespace Pets.Persistence
{
    using System;
    using System.Globalization;


    public static class Helpers
    {
        public static DateTime ParseDateTime(string dateTimeUtc)
        {
            DateTime dateTime =
                DateTime.ParseExact(dateTimeUtc, Constants.DateTimeFormats, CultureInfo.InvariantCulture);

            if (dateTime.Kind == DateTimeKind.Local)
                dateTime = dateTime.ToUniversalTime();
            
            return DateTime.SpecifyKind(
                dateTime,
                DateTimeKind.Utc);
        }

        public static string FormatDateTime(DateTime dateTimeUtc)
        {
            return dateTimeUtc.ToString(Constants.DateTimeFormat);
        }
    }
}