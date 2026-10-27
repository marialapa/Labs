using Labs.Utils;
using Labs.Validation;

namespace Labs.Tasks
{
    // Вариант 7: сумма всех натуральных чисел от 1 до N,
    // где N - длина пересечения двух отрезков дат (концы включаются)
    public static class Task2Calculator
    {
        // Ограничение на N: глубина рекурсии остаётся безопасной для стека,
        // а сумма 1 + ... + 10000 = 50 005 000 гарантированно помещается в long
        public const int MaxDays = 10000;

        // --task 2: рекурсивный расчёт по пересечению отрезков дат (Лабораторная работа 2)
        public static void Run(CmdArgsParser parser)
        {
            DateTime dtFirstStart = parser.GetDate("-d1st", "D1ST");
            DateTime dtFirstEnd = parser.GetDate("-d1end", "D1END");
            DateTime dtSecondStart = parser.GetDate("-d2st", "D2ST");
            DateTime dtSecondEnd = parser.GetDate("-d2end", "D2END");

            ValidateSegment(dtFirstStart, dtFirstEnd, "first");
            ValidateSegment(dtSecondStart, dtSecondEnd, "second");

            int iDays = GetIntersectionDays(dtFirstStart, dtFirstEnd, dtSecondStart, dtSecondEnd);
            long lResult = Calculate(iDays);

            ConsoleOutput.PrintDays(iDays);
            ConsoleOutput.PrintResult(lResult);
        }

        public static void ValidateSegment(DateTime dtStart, DateTime dtEnd, string sSegmentName)
        {
            if (dtEnd < dtStart)
            {
                throw new ValidationException($"End of the {sSegmentName} segment must not be earlier than its start.");
            }
        }

        public static int GetIntersectionDays(DateTime dtFirstStart, DateTime dtFirstEnd, DateTime dtSecondStart, DateTime dtSecondEnd)
        {
            DateTime dtIntersectionStart = dtFirstStart > dtSecondStart ? dtFirstStart : dtSecondStart;
            DateTime dtIntersectionEnd = dtFirstEnd < dtSecondEnd ? dtFirstEnd : dtSecondEnd;

            if (dtIntersectionEnd < dtIntersectionStart)
            {
                return 0;
            }

            return (dtIntersectionEnd - dtIntersectionStart).Days + 1;
        }

        public static long Calculate(int iDays)
        {
            if (iDays < 0)
            {
                throw new ValidationException("Intersection length N must not be negative.");
            }

            if (iDays > MaxDays)
            {
                throw new ValidationException($"Intersection length N = {iDays} exceeds the limit of {MaxDays} days.");
            }

            return SumNaturalNumbers(iDays);
        }

        // S(0) = 0; S(n) = n + S(n - 1)
        public static long SumNaturalNumbers(int iNumber)
        {
            if (iNumber <= 0)
            {
                return 0;
            }

            return iNumber + SumNaturalNumbers(iNumber - 1);
        }
    }
}
