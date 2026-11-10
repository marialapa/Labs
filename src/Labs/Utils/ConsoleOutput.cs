namespace Labs.Utils
{
    public static class ConsoleOutput
    {
        public static void PrintText(string sText)
        {
            Console.WriteLine(sText);
        }

        public static void PrintError(string sMessage)
        {
            Console.WriteLine($"ERROR: {sMessage}");
        }
    }
}
