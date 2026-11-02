namespace Labs.Utils
{
    // Ошибка чтения или преобразования аргумента командной строки.
    public class InputException : Exception
    {
        public InputException(string sMessage) : base(sMessage)
        {
        }
    }
}
