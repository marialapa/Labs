using System.Globalization;
using Labs.Tasks;

namespace Labs.Utils
{
    // Весь вывод программы на экран
    public static class ConsoleOutput
    {
        public static void PrintHelp()
        {
            Console.WriteLine("HELP:");
            Console.WriteLine("    --task - task number (1, 2 or 3)");
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
            Console.WriteLine("    Task 3: string checks (EQUAL, NORMALIZED, REVERSED, EMAIL / PHONE / IP for each string)");
            Console.WriteLine("    -s1 - first string");
            Console.WriteLine("    -s2 - second string");
            Console.WriteLine();
            Console.WriteLine("    Examples:");
            Console.WriteLine("        Labs --task 1 -x 1 -y 2 -z 3");
            Console.WriteLine("        Labs --task 2 -d1st 12.12.2020 -d1end 17.12.2020 -d2st 10.12.2020 -d2end 15.12.2020");
            Console.WriteLine("        Labs --task 3 -s1 \"Hello, world!\" -s2 \"hello world\"");
        }

        public static void PrintError(string sMessage)
        {
            Console.WriteLine($"ERROR: {sMessage}");
        }

        public static void PrintDays(int iDays)
        {
            Console.WriteLine($"DAYS: {iDays}");
        }

        public static void PrintResult(double dResult)
        {
            Console.WriteLine($"RESULT: {dResult.ToString("0.000", CultureInfo.InvariantCulture)}");
        }

        public static void PrintCheck(string sLabel, bool bValue)
        {
            string sValue = bValue ? "TRUE" : "FALSE";
            Console.WriteLine($"{sLabel}: {sValue}");
        }
    }
}
