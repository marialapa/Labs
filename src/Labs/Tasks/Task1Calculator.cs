using Labs.Utils;
using Labs.Validation;

namespace Labs.Tasks
{
    // Вариант 14: (Y % X) / sqrt(Z)
    public static class Task1Calculator
    {
        // --task 1: расчёт формулы (Лабораторная работа 1)
        public static void Run(CmdArgsParser parser)
        {
            int iX = parser.GetInt("-x", "X");
            int iY = parser.GetInt("-y", "Y");
            int iZ = parser.GetInt("-z", "Z");

            double dResult = Calculate(iX, iY, iZ);

            ConsoleOutput.PrintResult(dResult);
        }

        public static double Calculate(int iX, int iY, int iZ)
        {
            if (iX == 0)
            {
                throw new ValidationException("Value X must not be 0.");
            }

            if (iZ < 0)
            {
                throw new ValidationException("Value Z must not be negative.");
            }

            if (iZ == 0)
            {
                throw new ValidationException("Value Z must not be 0.");
            }

            return (iY % iX) / Math.Sqrt(iZ);
        }
    }
}
