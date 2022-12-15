using System;

namespace Company.DataSource
{
    public class DataError : Exception
    {
        public const int InterpreterError = 1;
        public const int ParserError = 2;
        public const int InvalidData = 3;

        public int ErrorCode { get; }

        public DataError(int errorCode)
            : base($"Data error: {errorCode}")
        {
            ErrorCode = errorCode;
        }
    }
}