using Labs.Validation;

namespace Labs.Calculations
{
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

            return ((long)iY % iX) / Math.Sqrt(iZ);
        }
    }
}
