using Labs.Calculations;

namespace Labs.Tasks
{
    public class StringCheckTask : LabTask
    {
        private readonly string m_sFirst;
        private readonly string m_sSecond;

        public StringCheckTask(string sFirst, string sSecond)
        {
            m_sFirst = sFirst;
            m_sSecond = sSecond;
        }

        public override IReadOnlyList<string> Execute()
        {
            List<string> lines = new List<string>();
            lines.Add(Check("EQUAL", () => Task3StringChecker.ValidateEqual(m_sFirst, m_sSecond)));
            lines.Add(Check("NORMALIZED", () => Task3StringChecker.ValidateNormalizedEqual(m_sFirst, m_sSecond)));
            lines.Add(Check("REVERSED", () => Task3StringChecker.ValidateReversed(m_sFirst, m_sSecond)));
            AddStringTypes(lines, "S1", m_sFirst);
            AddStringTypes(lines, "S2", m_sSecond);
            return lines;
        }

        private static void AddStringTypes(List<string> lines, string sPrefix, string sValue)
        {
            lines.Add(Check($"{sPrefix}_EMAIL", () => Task3StringChecker.ValidateEmail(sValue)));
            lines.Add(Check($"{sPrefix}_PHONE", () => Task3StringChecker.ValidatePhone(sValue)));
            lines.Add(Check($"{sPrefix}_IP", () => Task3StringChecker.ValidateIp(sValue)));
        }
    }
}
