using System.Globalization;
using Labs.Validation;

namespace Labs.Tasks
{
    public abstract class LabTask
    {
        public abstract IReadOnlyList<string> Execute();

        protected static string FormatResult(double dResult)
        {
            return $"RESULT: {dResult.ToString("0.000", CultureInfo.InvariantCulture)}";
        }

        protected static string Check(string sLabel, Action actValidation)
        {
            bool bPassed;
            try
            {
                actValidation();
                bPassed = true;
            }
            catch (ValidationException)
            {
                bPassed = false;
            }

            return $"{sLabel}: {(bPassed ? "TRUE" : "FALSE")}";
        }
    }
}
