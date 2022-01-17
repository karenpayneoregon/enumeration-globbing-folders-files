using System;

namespace DirectoryHelpersLibrary.Classes
{
    public class DateHelper
    {
        public static string CalculateExpirationTime(DateTime expiryDate)
        {
            var currentDate = DateTime.Now;
            var dateDifference = (expiryDate - currentDate);

            if (dateDifference.Days >= 1)
                return $"{ dateDifference.Days } day(s) remained";
            else if (dateDifference.Hours >= 1)
                return $"{ dateDifference.Hours } hour(s) remained";
            else if (dateDifference.Minutes >= 1)
                return $"{ dateDifference.Minutes } minute(s) remained";
            else if (dateDifference.TotalSeconds >= 1)
                return $"{ dateDifference.Seconds } second(s) remained";

            return "Expired!";
        }
    }
}
