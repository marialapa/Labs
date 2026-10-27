using Labs.Tasks;
using Labs.Utils;
using Labs.Validation;

namespace Labs
{
    public class Program
    {
        private const int MinTaskNumber = 1;
        private const int MaxTaskNumber = 3;

        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                ConsoleOutput.PrintHelp();
                return;
            }

            // Единая точка обработки ошибок: любая ValidationException
            // превращается в строку "ERROR: <пояснение>"
            try
            {
                CmdArgsParser parser = new CmdArgsParser(args);
                int iTask = parser.GetTaskNumber(MinTaskNumber, MaxTaskNumber);

                switch (iTask)
                {
                    case 1:
                        Task1Calculator.Run(parser);
                        break;
                    case 2:
                        Task2Calculator.Run(parser);
                        break;
                    case 3:
                        Task3StringChecker.Run(parser);
                        break;
                    default:
                        throw new ValidationException($"Unknown task {iTask}.");
                }
            }
            catch (ValidationException exValidation)
            {
                ConsoleOutput.PrintError(exValidation.Message);
            }
            catch (Exception exUnexpected)
            {
                ConsoleOutput.PrintError($"Unexpected error: {exUnexpected.Message}");
            }
        }
    }
}
