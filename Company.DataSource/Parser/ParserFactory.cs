namespace Company.DataSource
{
    /// <summary>
    /// Support to create a <see cref="DataParser"/> instance
    /// </summary>
    public class ParserFactory
    {
        /// <summary>
        /// Create a <see cref="DataParser"/> instance based on <see cref="DataType"/>
        /// </summary>
        /// <param name="type"></param>
        /// <returns>created instance</returns>
        /// <exception cref="DataError">cannot create requested instance</exception>
        public static DataParser Create(DataType type)
        {
            var interpreter = InterpreterFactory.Create(type);
            switch (type)
            {
                case DataType.Comma:
                    return new CommaParser { Interpreter = interpreter };
                case DataType.Hash:
                    return new HastParser { Interpreter = interpreter };
                case DataType.Hyphen:
                    return new HyphenParser { Interpreter = interpreter };
                default:
                    throw new DataError(DataError.ParserError);
            }

        }
    }
}