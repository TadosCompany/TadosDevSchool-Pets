namespace Pets.Persistence
{
    using System;
    using System.Globalization;


    public static class Helpers
    {
        public static DateTime ParseDateTime(string dateTimeUtc)
        {
            return DateTime.SpecifyKind(
                DateTime.ParseExact(dateTimeUtc, Constants.DateTimeFormat, CultureInfo.InvariantCulture),
                DateTimeKind.Utc);
        }

        public static string FormatDateTime(DateTime dateTimeUtc)
        {
            return dateTimeUtc.ToString(Constants.DateTimeFormat);
        }
    }
}