using Labs.Validation;

namespace Labs.Calculations
{
    // Вариант 14: (Y % X) / sqrt(Z)
    public static class Task1Calculator
    {
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

            // Приведение к long до операции % предотвращает переполнение
            // для int.MinValue % -1, математический остаток которого равен 0.
            return ((long)iY % iX) / Math.Sqrt(iZ);
        }
    }
}
