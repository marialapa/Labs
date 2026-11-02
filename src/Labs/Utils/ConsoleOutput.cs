namespace Labs.Utils
{
    // Вывод готового текста, без зависимостей от заданий и вычислений.
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
