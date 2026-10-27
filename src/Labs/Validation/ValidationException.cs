namespace Labs.Validation
{
    // Исключение для ошибок проверки входных данных.
    // Пояснение к ошибке хранится в унаследованном свойстве Message
    public class ValidationException : Exception
    {
        public ValidationException() : base()
        {
        }

        public ValidationException(string sMessage) : base(sMessage)
        {
        }

        public ValidationException(string sMessage, Exception exInner) : base(sMessage, exInner)
        {
        }
    }
}
