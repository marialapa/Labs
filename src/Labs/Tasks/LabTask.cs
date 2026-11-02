using System.Globalization;
using Labs.Validation;

namespace Labs.Tasks
{
    // Общий контракт заданий: получить строки результата без чтения и вывода данных.
    public abstract class LabTask
    {
        public abstract IReadOnlyList<string> Execute();

        // Единый формат численного результата для заданий 1 и 2.
        protected static string FormatResult(double dResult)
        {
            return $"RESULT: {dResult.ToString("0.000", CultureInfo.InvariantCulture)}";
        }

        // Обработка ValidationException из ЛР4: неуспешная проверка даёт FALSE.
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
