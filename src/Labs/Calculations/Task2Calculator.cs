using Labs.Validation;

namespace Labs.Calculations
{
    public static class Task2Calculator
    {
        public const int MaxDays = 10000;

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
