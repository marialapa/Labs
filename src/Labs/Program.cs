using System.Globalization;
using Labs.Tasks;
using Labs.Utils;

namespace Labs
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                PrintHelp();
                return;
            }

            var parsed = CmdArgsParser.Parse(args);

            if (!TryParseTaskNumber(parsed, out int task, out string? taskError))
            {
                Console.WriteLine($"ERROR: {taskError}");
                return;
            }

            if (task < 1)
            {
                Console.WriteLine("ERROR: Unknown task. Task number cannot be less than 1.");
                return;
            }

            if (task > 1)
            {
                Console.WriteLine("ERROR: Unknown task. Task number must not exceed 1.");
                return;
            }

            if (!TryParseIntArg(parsed, "-x", "X", out int x, out string? xError))
            {
                Console.WriteLine($"ERROR: {xError}");
                return;
            }

            if (!TryParseIntArg(parsed, "-y", "Y", out int y, out string? yError))
            {
                Console.WriteLine($"ERROR: {yError}");
                return;
            }

            if (!TryParseIntArg(parsed, "-z", "Z", out int z, out string? zError))
            {
                Console.WriteLine($"ERROR: {zError}");
                return;
            }

            if (!Task1Calculator.TryCalculate(x, y, z, out double result, out string? calcError))
            {
                Console.WriteLine($"ERROR: {calcError}");
                return;
            }

            Console.WriteLine($"RESULT: {result.ToString("0.000", CultureInfo.InvariantCulture)}");
        }

        private static void PrintHelp()
        {
            Console.WriteLine("HELP:");
            Console.WriteLine("    --task - task number");
            Console.WriteLine("    -x - value of X");
            Console.WriteLine("    -y - value of Y");
            Console.WriteLine("    -z - value of Z");
            Console.WriteLine("    Task 1: calculates (Y % X) / sqrt(Z)");
        }

        private static bool TryParseTaskNumber(Dictionary<string, string?> args, out int task, out string? error)
        {
            task = 0;
            error = null;

            if (!args.TryGetValue("--task", out var raw) || raw is null)
            {
                error = "Task number is required.";
                return false;
            }

            if (!int.TryParse(raw, NumberStyles.Integer, CultureInfo.InvariantCulture, out task))
            {
                error = "Task number must be an integer.";
                return false;
            }

            return true;
        }

        private static bool TryParseIntArg(Dictionary<string, string?> args, string key, string name, out int value, out string? error)
        {
            value = 0;
            error = null;

            if (!args.TryGetValue(key, out var raw) || raw is null)
            {
                error = $"Value {name} is required.";
                return false;
            }

            if (!int.TryParse(raw, NumberStyles.Integer, CultureInfo.InvariantCulture, out value))
            {
                error = $"Value {name} must be an integer.";
                return false;
            }

            return true;
        }
    }
}
