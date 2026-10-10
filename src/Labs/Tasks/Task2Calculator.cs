namespace Labs.Tasks
{
    // Вариант 7: сумма всех натуральных чисел от 1 до N,
    // где N - длина пересечения двух отрезков дат (концы включаются)
    public static class Task2Calculator
    {
        // Ограничение на N: глубина рекурсии остаётся безопасной для стека,
        // а сумма 1 + ... + 10000 = 50 005 000 гарантированно помещается в long
        public const int MaxDays = 10000;

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

        public static bool TryCalculate(int iDays, out long lResult, out string? sError)
        {
            lResult = 0;
            sError = null;

            if (iDays < 0)
            {
                sError = "Intersection length N must not be negative.";
                return false;
            }

            if (iDays > MaxDays)
            {
                sError = $"Intersection length N = {iDays} exceeds the limit of {MaxDays} days.";
                return false;
            }

            lResult = SumNaturalNumbers(iDays);
            return true;
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
