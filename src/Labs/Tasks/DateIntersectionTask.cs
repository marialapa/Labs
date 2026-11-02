using Labs.Calculations;

namespace Labs.Tasks
{
    // Задание 2 получает четыре даты, а не аргументы командной строки.
    public class DateIntersectionTask : LabTask
    {
        private readonly DateTime m_dtFirstStart;
        private readonly DateTime m_dtFirstEnd;
        private readonly DateTime m_dtSecondStart;
        private readonly DateTime m_dtSecondEnd;

        public DateIntersectionTask(DateTime dtFirstStart, DateTime dtFirstEnd, DateTime dtSecondStart, DateTime dtSecondEnd)
        {
            m_dtFirstStart = dtFirstStart;
            m_dtFirstEnd = dtFirstEnd;
            m_dtSecondStart = dtSecondStart;
            m_dtSecondEnd = dtSecondEnd;
        }

        public override IReadOnlyList<string> Execute()
        {
            Task2Calculator.ValidateSegment(m_dtFirstStart, m_dtFirstEnd, "first");
            Task2Calculator.ValidateSegment(m_dtSecondStart, m_dtSecondEnd, "second");
            int iDays = Task2Calculator.GetIntersectionDays(m_dtFirstStart, m_dtFirstEnd, m_dtSecondStart, m_dtSecondEnd);
            long lResult = Task2Calculator.Calculate(iDays);
            return new[] { $"DAYS: {iDays}", FormatResult(lResult) };
        }
    }
}
