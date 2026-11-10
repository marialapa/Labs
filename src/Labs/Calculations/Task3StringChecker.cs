using System.Text.RegularExpressions;
using Labs.Validation;

namespace Labs.Calculations
{
    public static class Task3StringChecker
    {
        private static readonly Regex s_rxEmail = new Regex(@"\A(?:[^@ \t\r\n]+@[^@ \t\r\n]+\.[^@ \t\r\n]+)\z");
        private static readonly Regex s_rxIp = new Regex(@"\A(?:(\b25[0-5]|\b2[0-4][0-9]|\b[01]?[0-9][0-9]?)(\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)){3})\z");
        private static readonly Regex s_rxPhone = new Regex(@"\A[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}\z");
        private static readonly Regex s_rxMultipleSpaces = new Regex(@"\s{2,}");

        public static void ValidateEqual(string sFirst, string sSecond)
        {
            if (!string.Equals(sFirst, sSecond, StringComparison.Ordinal))
            {
                throw new ValidationException("Strings are not equal character by character.");
            }
        }

        public static void ValidateNormalizedEqual(string sFirst, string sSecond)
        {
            string sFirstNormalized = NormalizeString(sFirst);
            string sSecondNormalized = NormalizeString(sSecond);

            if (!string.Equals(sFirstNormalized, sSecondNormalized, StringComparison.Ordinal))
            {
                throw new ValidationException("Strings are not equal after normalization.");
            }
        }

        public static void ValidateReversed(string sFirst, string sSecond)
        {
            string sSecondReversed = ReverseString(sSecond);

            if (!string.Equals(sFirst, sSecondReversed, StringComparison.Ordinal))
            {
                throw new ValidationException("The first string is not the reverse of the second one.");
            }
        }

        public static void ValidateEmail(string sValue)
        {
            if (!s_rxEmail.IsMatch(sValue))
            {
                throw new ValidationException($"'{sValue}' is not an email address.");
            }
        }

        public static void ValidatePhone(string sValue)
        {
            if (!s_rxPhone.IsMatch(sValue))
            {
                throw new ValidationException($"'{sValue}' is not a phone number.");
            }
        }

        public static void ValidateIp(string sValue)
        {
            if (!s_rxIp.IsMatch(sValue))
            {
                throw new ValidationException($"'{sValue}' is not an IP address.");
            }
        }

        public static string NormalizeString(string sValue)
        {
            string sLower = sValue.ToLowerInvariant();
            string sTrimmed = sLower.Trim();

            return s_rxMultipleSpaces.Replace(sTrimmed, " ");
        }

        public static string ReverseString(string sValue)
        {
            char[] arrChars = sValue.ToCharArray();
            Array.Reverse(arrChars);

            return new string(arrChars);
        }

    }
}
