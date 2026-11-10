namespace Labs.Validation
{
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
