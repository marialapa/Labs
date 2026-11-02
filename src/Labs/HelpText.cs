namespace Labs
{
    // Описание режимов приложения. Утилиты вывода получают уже готовый текст.
    public static class HelpText
    {
        public static string GetText()
        {
            List<string> lines = new List<string>();
            lines.Add("HELP:");
            lines.Add("    --task - task number (1, 2 or 3)");
            lines.Add(string.Empty);
            lines.Add("    Task 1: calculates (Y % X) / sqrt(Z)");
            lines.Add("    -x - value of X (integer)");
            lines.Add("    -y - value of Y (integer)");
            lines.Add("    -z - value of Z (integer)");
            lines.Add(string.Empty);
            lines.Add("    Task 2: N = number of days in the intersection of two date segments (ends included),");
            lines.Add($"            result = sum of natural numbers from 1 to N (recursive), N <= {Calculations.Task2Calculator.MaxDays}");
            lines.Add("    -d1st - start of the first date segment (DD.MM.YYYY)");
            lines.Add("    -d1end - end of the first date segment (DD.MM.YYYY)");
            lines.Add("    -d2st - start of the second date segment (DD.MM.YYYY)");
            lines.Add("    -d2end - end of the second date segment (DD.MM.YYYY)");
            lines.Add(string.Empty);
            lines.Add("    Task 3: string checks (EQUAL, NORMALIZED, REVERSED, EMAIL / PHONE / IP for each string)");
            lines.Add("    -s1 - first string");
            lines.Add("    -s2 - second string");
            lines.Add(string.Empty);
            lines.Add("    Examples:");
            lines.Add("        Labs --task 1 -x 1 -y 2 -z 3");
            lines.Add("        Labs --task 2 -d1st 12.12.2020 -d1end 17.12.2020 -d2st 10.12.2020 -d2end 15.12.2020");
            lines.Add("        Labs --task 3 -s1 \"Hello, world!\" -s2 \"hello world\"");
            return string.Join(Environment.NewLine, lines);
        }
    }
}
