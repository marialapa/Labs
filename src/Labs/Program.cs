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
                ConsoleOutput.PrintText(HelpText.GetText());
                return;
            }

            try
            {
                CmdArgsParser parser = new CmdArgsParser(args);
                int iTask = parser.GetTaskNumber(MinTaskNumber, MaxTaskNumber);

                LabTask task;
                switch (iTask)
                {
                    case 1:
                        task = new FormulaTask(
                            parser.GetInt("-x", "X"),
                            parser.GetInt("-y", "Y"),
                            parser.GetInt("-z", "Z"));
                        break;
                    case 2:
                        task = new DateIntersectionTask(
                            parser.GetDate("-d1st", "D1ST"),
                            parser.GetDate("-d1end", "D1END"),
                            parser.GetDate("-d2st", "D2ST"),
                            parser.GetDate("-d2end", "D2END"));
                        break;
                    case 3:
                        task = new StringCheckTask(
                            parser.GetString("-s1", "S1"),
                            parser.GetString("-s2", "S2"));
                        break;
                    default:
                        throw new ValidationException($"Unknown task {iTask}.");
                }

                foreach (string sLine in task.Execute())
                {
                    ConsoleOutput.PrintText(sLine);
                }
            }
            catch (InputException exInput)
            {
                ConsoleOutput.PrintError(exInput.Message);
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
