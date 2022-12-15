namespace Company.DataSource
{
    /// <summary>
    /// Support to create a <see cref="IDataInterpreter"/> instance
    /// </summary>
    public class InterpreterFactory
    {
        /// <summary>
        /// Create a <see cref="IDataInterpreter"/> instance based on <see cref="DataType"/>
        /// </summary>
        /// <param name="type"></param>
        /// <returns>created instance</returns>
        /// <exception cref="DataError">cannot create requested instance</exception>
        public static IDataInterpreter Create(DataType type)
        {
            switch (type)
            {
                case DataType.Comma:
                    return new CommaInterpreter();
                case DataType.Hash:
                    return new HashInterpreter();
                case DataType.Hyphen:
                    return new HyphenInterpreter();
                default:
                    throw new DataError(DataError.InterpreterError);
            }
        }
    }
}