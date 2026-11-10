using Labs.Calculations;

namespace Labs.Tasks
{
    public class FormulaTask : LabTask
    {
        private readonly int m_iX;
        private readonly int m_iY;
        private readonly int m_iZ;

        public FormulaTask(int iX, int iY, int iZ)
        {
            m_iX = iX;
            m_iY = iY;
            m_iZ = iZ;
        }

        public override IReadOnlyList<string> Execute()
        {
            double dResult = Task1Calculator.Calculate(m_iX, m_iY, m_iZ);
            return new[] { FormatResult(dResult) };
        }
    }
}
