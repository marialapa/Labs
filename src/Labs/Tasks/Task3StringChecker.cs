using System.Text.RegularExpressions;

namespace Labs.Tasks
{
    // Проверки строк. Методы не читают консоль и аргументы:
    // принимают строки параметрами и возвращают результат
    public static class Task3StringChecker
    {
        // Шаблоны из задания, заякорены ^...$ - совпадение должно быть по всей строке
        private static readonly Regex s_rxEmail = new Regex(@"^(?:[^@ \t\r\n]+@[^@ \t\r\n]+\.[^@ \t\r\n]+)$");
        private static readonly Regex s_rxIp = new Regex(@"^(?:(\b25[0-5]|\b2[0-4][0-9]|\b[01]?[0-9][0-9]?)(\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)){3})$");
        private static readonly Regex s_rxPhone = new Regex(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$");
        private static readonly Regex s_rxMultipleSpaces = new Regex(@"\s{2,}");

        // EQUAL: посимвольное совпадение с учётом регистра
        public static bool IsEqual(string sFirst, string sSecond)
        {
            return string.Equals(sFirst, sSecond, StringComparison.Ordinal);
        }

        // NORMALIZED: совпадение после нормализации обеих строк
        public static bool IsNormalizedEqual(string sFirst, string sSecond)
        {
            string sFirstNormalized = NormalizeString(sFirst);
            string sSecondNormalized = NormalizeString(sSecond);

            return string.Equals(sFirstNormalized, sSecondNormalized, StringComparison.Ordinal);
        }

        // REVERSED: первая строка - вторая, записанная задом наперёд
        public static bool IsReversed(string sFirst, string sSecond)
        {
            string sSecondReversed = ReverseString(sSecond);

            return string.Equals(sFirst, sSecondReversed, StringComparison.Ordinal);
        }

        public static bool IsEmail(string sValue)
        {
            return s_rxEmail.IsMatch(sValue);
        }

        public static bool IsPhone(string sValue)
        {
            return s_rxPhone.IsMatch(sValue);
        }

        public static bool IsIp(string sValue)
        {
            return s_rxIp.IsMatch(sValue);
        }

        // Один регистр, без пробелов по краям, 2+ пробельных символа подряд -> один пробел
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
