namespace Labs.Tasks
{
    // Вариант 14: (Y % X) / sqrt(Z)
    public static class Task1Calculator
    {
        public static bool TryCalculate(int x, int y, int z, out double result, out string? error)
        {
            result = 0;
            error = null;

            if (x == 0)
            {
                error = "Value X must not be 0.";
                return false;
            }

            if (z < 0)
            {
                error = "Value Z must not be negative.";
                return false;
            }

            if (z == 0)
            {
                error = "Value Z must not be 0.";
                return false;
            }

            result = (y % x) / Math.Sqrt(z);
            return true;
        }
    }
}
