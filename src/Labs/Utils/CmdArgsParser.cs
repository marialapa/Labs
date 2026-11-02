using System.Globalization;
using System.Text.RegularExpressions;

namespace Labs.Utils
{
    // Разбор и проверка аргументов командной строки.
    // При некорректном значении методы Get* генерируют InputException
    public class CmdArgsParser
    {
        private const string TaskKey = "--task";
        private const string DateFormat = "dd.MM.yyyy";

        private readonly Dictionary<string, string?> m_dictValues;

        public CmdArgsParser(string[] args)
        {
            m_dictValues = Parse(args);
        }

        public static Dictionary<string, string?> Parse(string[] args)
        {
            Dictionary<string, string?> values = new Dictionary<string, string?>();
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].StartsWith("-", StringComparison.Ordinal))
                {
                    string sKey = args[i];
                    if (i + 1 < args.Length)
                    {
                        // Значение уже принадлежит текущему ключу, даже если
                        // начинается с дефиса (например, -1 или строка --task).
                        i++;
                        values[sKey] = args[i];
                    }
                    else
                    {
                        values[sKey] = null;
                    }
                }
            }
            return values;
        }

        public int GetTaskNumber(int iMinTask, int iMaxTask)
        {
            if (!m_dictValues.TryGetValue(TaskKey, out var sRaw) || sRaw is null)
            {
                throw new InputException("Task number is required.");
            }

            if (!int.TryParse(sRaw, NumberStyles.Integer, CultureInfo.InvariantCulture, out int iTask))
            {
                throw new InputException("Task number must be an integer.");
            }

            if (iTask < iMinTask)
            {
                throw new InputException($"Unknown task. Task number cannot be less than {iMinTask}.");
            }

            if (iTask > iMaxTask)
            {
                throw new InputException($"Unknown task. Task number must not exceed {iMaxTask}.");
            }

            return iTask;
        }

        public int GetInt(string sKey, string sName)
        {
            if (!m_dictValues.TryGetValue(sKey, out var sRaw) || sRaw is null)
            {
                throw new InputException($"Value {sName} is required.");
            }

            if (!int.TryParse(sRaw, NumberStyles.Integer, CultureInfo.InvariantCulture, out int iValue))
            {
                throw new InputException($"Value {sName} must be an integer.");
            }

            return iValue;
        }

        public string GetString(string sKey, string sName)
        {
            if (!m_dictValues.TryGetValue(sKey, out var sRaw) || sRaw is null)
            {
                throw new InputException($"Value {sName} must be specified.");
            }

            return sRaw;
        }

        public DateTime GetDate(string sKey, string sName)
        {
            if (!m_dictValues.TryGetValue(sKey, out var sRaw) || sRaw is null)
            {
                throw new InputException($"Value {sName} is required.");
            }

            // Строго ДД.ММ.ГГГГ: 1.09.2020, 10.9.2020 и 10.10.20 - некорректны
            bool bFormatOk = Regex.IsMatch(sRaw, @"^\d{2}\.\d{2}\.\d{4}$");
            bool bDateOk = DateTime.TryParseExact(sRaw, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dtValue);

            if (!bFormatOk || !bDateOk)
            {
                throw new InputException($"Value {sName} must be a valid date in format DD.MM.YYYY.");
            }

            return dtValue;
        }
    }
}
