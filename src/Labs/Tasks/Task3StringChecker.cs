using System.Text.RegularExpressions;
using Labs.Utils;
using Labs.Validation;

namespace Labs.Tasks
{
    // Проверки строк. Методы Validate* не читают консоль и аргументы:
    // принимают строки параметрами и при неуспешной проверке генерируют ValidationException
    public static class Task3StringChecker
    {
        // Шаблоны из задания, заякорены ^...$ - совпадение должно быть по всей строке
        private static readonly Regex s_rxEmail = new Regex(@"^(?:[^@ \t\r\n]+@[^@ \t\r\n]+\.[^@ \t\r\n]+)$");
        private static readonly Regex s_rxIp = new Regex(@"^(?:(\b25[0-5]|\b2[0-4][0-9]|\b[01]?[0-9][0-9]?)(\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)){3})$");
        private static readonly Regex s_rxPhone = new Regex(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$");
        private static readonly Regex s_rxMultipleSpaces = new Regex(@"\s{2,}");

        // --task 3: проверки строк (Лабораторные работы 3 и 4)
        public static void Run(CmdArgsParser parser)
        {
            string sFirst = parser.GetString("-s1", "S1");
            string sSecond = parser.GetString("-s2", "S2");

            ConsoleOutput.PrintCheck("EQUAL", IsPassed(() => ValidateEqual(sFirst, sSecond)));
            ConsoleOutput.PrintCheck("NORMALIZED", IsPassed(() => ValidateNormalizedEqual(sFirst, sSecond)));
            ConsoleOutput.PrintCheck("REVERSED", IsPassed(() => ValidateReversed(sFirst, sSecond)));
            PrintStringTypes("S1", sFirst);
            PrintStringTypes("S2", sSecond);
        }

        // Обработчик ошибок проверки: выполняет проверку и превращает
        // ValidationException в FALSE, успешное выполнение - в TRUE
        public static bool IsPassed(Action actValidation)
        {
            try
            {
                actValidation();
                return true;
            }
            catch (ValidationException)
            {
                return false;
            }
        }

        // EQUAL: посимвольное совпадение с учётом регистра
        public static void ValidateEqual(string sFirst, string sSecond)
        {
            if (!string.Equals(sFirst, sSecond, StringComparison.Ordinal))
            {
                throw new ValidationException("Strings are not equal character by character.");
            }
        }

        // NORMALIZED: совпадение после нормализации обеих строк
        public static void ValidateNormalizedEqual(string sFirst, string sSecond)
        {
            string sFirstNormalized = NormalizeString(sFirst);
            string sSecondNormalized = NormalizeString(sSecond);

            if (!string.Equals(sFirstNormalized, sSecondNormalized, StringComparison.Ordinal))
            {
                throw new ValidationException("Strings are not equal after normalization.");
            }
        }

        // REVERSED: первая строка - вторая, записанная задом наперёд
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

        private static void PrintStringTypes(string sPrefix, string sValue)
        {
            ConsoleOutput.PrintCheck($"{sPrefix}_EMAIL", IsPassed(() => ValidateEmail(sValue)));
            ConsoleOutput.PrintCheck($"{sPrefix}_PHONE", IsPassed(() => ValidatePhone(sValue)));
            ConsoleOutput.PrintCheck($"{sPrefix}_IP", IsPassed(() => ValidateIp(sValue)));
        }
    }
}
