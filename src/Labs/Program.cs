using System.Globalization;
using System.Text.RegularExpressions;
using Labs.Tasks;
using Labs.Utils;

namespace Labs
{
    public class Program
    {
        private const int MinTaskNumber = 1;
        private const int MaxTaskNumber = 2;
        private const string DateFormat = "dd.MM.yyyy";

        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                PrintHelp();
                return;
            }

            var dictArgs = CmdArgsParser.Parse(args);

            if (!TryParseTaskNumber(dictArgs, out int iTask, out string? sTaskError))
            {
                PrintError(sTaskError);
                return;
            }

            switch (iTask)
            {
                case 1:
                    RunTask1(dictArgs);
                    break;
                case 2:
                    RunTask2(dictArgs);
                    break;
                default:
                    PrintError(GetUnknownTaskMessage(iTask));
                    break;
            }
        }

        // --task 1: расчёт формулы (Лабораторная работа 1)
        private static void RunTask1(Dictionary<string, string?> dictArgs)
        {
            if (!TryParseIntArg(dictArgs, "-x", "X", out int iX, out string? sXError))
            {
                PrintError(sXError);
                return;
            }

            if (!TryParseIntArg(dictArgs, "-y", "Y", out int iY, out string? sYError))
            {
                PrintError(sYError);
                return;
            }

            if (!TryParseIntArg(dictArgs, "-z", "Z", out int iZ, out string? sZError))
            {
                PrintError(sZError);
                return;
            }

            if (!Task1Calculator.TryCalculate(iX, iY, iZ, out double dResult, out string? sCalcError))
            {
                PrintError(sCalcError);
                return;
            }

            PrintResult(dResult);
        }

        // --task 2: рекурсивный расчёт по пересечению отрезков дат (Лабораторная работа 2)
        private static void RunTask2(Dictionary<string, string?> dictArgs)
        {
            if (!TryParseDateArg(dictArgs, "-d1st", "D1ST", out DateTime dtFirstStart, out string? sError))
            {
                PrintError(sError);
                return;
            }

            if (!TryParseDateArg(dictArgs, "-d1end", "D1END", out DateTime dtFirstEnd, out sError))
            {
                PrintError(sError);
                return;
            }

            if (!TryParseDateArg(dictArgs, "-d2st", "D2ST", out DateTime dtSecondStart, out sError))
            {
                PrintError(sError);
                return;
            }

            if (!TryParseDateArg(dictArgs, "-d2end", "D2END", out DateTime dtSecondEnd, out sError))
            {
                PrintError(sError);
                return;
            }

            if (dtFirstEnd < dtFirstStart)
            {
                PrintError("End of the first segment must not be earlier than its start.");
                return;
            }

            if (dtSecondEnd < dtSecondStart)
            {
                PrintError("End of the second segment must not be earlier than its start.");
                return;
            }

            int iDays = Task2Calculator.GetIntersectionDays(dtFirstStart, dtFirstEnd, dtSecondStart, dtSecondEnd);

            if (!Task2Calculator.TryCalculate(iDays, out long lResult, out string? sCalcError))
            {
                PrintError(sCalcError);
                return;
            }

            PrintDays(iDays);
            PrintResult(lResult);
        }

        private static void PrintHelp()
        {
            Console.WriteLine("HELP:");
            Console.WriteLine("    --task - task number (1 or 2)");
            Console.WriteLine();
            Console.WriteLine("    Task 1: calculates (Y % X) / sqrt(Z)");
            Console.WriteLine("    -x - value of X (integer)");
            Console.WriteLine("    -y - value of Y (integer)");
            Console.WriteLine("    -z - value of Z (integer)");
            Console.WriteLine();
            Console.WriteLine("    Task 2: N = number of days in the intersection of two date segments (ends included),");
            Console.WriteLine($"            result = sum of natural numbers from 1 to N (recursive), N <= {Task2Calculator.MaxDays}");
            Console.WriteLine("    -d1st - start of the first date segment (DD.MM.YYYY)");
            Console.WriteLine("    -d1end - end of the first date segment (DD.MM.YYYY)");
            Console.WriteLine("    -d2st - start of the second date segment (DD.MM.YYYY)");
            Console.WriteLine("    -d2end - end of the second date segment (DD.MM.YYYY)");
            Console.WriteLine();
            Console.WriteLine("    Examples:");
            Console.WriteLine("        Labs --task 1 -x 1 -y 2 -z 3");
            Console.WriteLine("        Labs --task 2 -d1st 12.12.2020 -d1end 17.12.2020 -d2st 10.12.2020 -d2end 15.12.2020");
        }

        private static void PrintError(string? sMessage)
        {
            Console.WriteLine($"ERROR: {sMessage}");
        }

        private static void PrintDays(int iDays)
        {
            Console.WriteLine($"DAYS: {iDays}");
        }

        private static void PrintResult(double dResult)
        {
            Console.WriteLine($"RESULT: {dResult.ToString("0.000", CultureInfo.InvariantCulture)}");
        }

        private static string GetUnknownTaskMessage(int iTask)
        {
            if (iTask < MinTaskNumber)
            {
                return $"Unknown task. Task number cannot be less than {MinTaskNumber}.";
            }

            return $"Unknown task. Task number must not exceed {MaxTaskNumber}.";
        }

        private static bool TryParseTaskNumber(Dictionary<string, string?> dictArgs, out int iTask, out string? sError)
        {
            iTask = 0;
            sError = null;

            if (!dictArgs.TryGetValue("--task", out var sRaw) || sRaw is null)
            {
                sError = "Task number is required.";
                return false;
            }

            if (!int.TryParse(sRaw, NumberStyles.Integer, CultureInfo.InvariantCulture, out iTask))
            {
                sError = "Task number must be an integer.";
                return false;
            }

            return true;
        }

        private static bool TryParseIntArg(Dictionary<string, string?> dictArgs, string sKey, string sName, out int iValue, out string? sError)
        {
            iValue = 0;
            sError = null;

            if (!dictArgs.TryGetValue(sKey, out var sRaw) || sRaw is null)
            {
                sError = $"Value {sName} is required.";
                return false;
            }

            if (!int.TryParse(sRaw, NumberStyles.Integer, CultureInfo.InvariantCulture, out iValue))
            {
                sError = $"Value {sName} must be an integer.";
                return false;
            }

            return true;
        }

        private static bool TryParseDateArg(Dictionary<string, string?> dictArgs, string sKey, string sName, out DateTime dtValue, out string? sError)
        {
            dtValue = DateTime.MinValue;
            sError = null;

            if (!dictArgs.TryGetValue(sKey, out var sRaw) || sRaw is null)
            {
                sError = $"Value {sName} is required.";
                return false;
            }

            // Строго ДД.ММ.ГГГГ: 1.09.2020, 10.9.2020 и 10.10.20 - некорректны
            bool bFormatOk = Regex.IsMatch(sRaw, @"^\d{2}\.\d{2}\.\d{4}$");
            bool bDateOk = DateTime.TryParseExact(sRaw, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out dtValue);

            if (!bFormatOk || !bDateOk)
            {
                sError = $"Value {sName} must be a valid date in format DD.MM.YYYY.";
                return false;
            }

            return true;
        }
    }
}
