using System.Collections.Generic;
using System.IO;

namespace Company.DataSource
{
    /// <summary>
    /// Creates <see cref="CompanyData"/> from on a stream content
    /// </summary>
    public abstract class DataParser
    {
        /// <summary>
        /// instance of <see cref="IDataInterpreter"/> used to create <see cref="CompanyData"/>
        /// </summary>
        public IDataInterpreter Interpreter { get; set; }

        /// <summary>
        /// Creates a list of <see cref="CompanyData"/> based on <paramref name="source"/>
        /// </summary>
        /// <param name="source">data stream that should contain expected data as text</param>
        /// <returns>created list</returns>
        public List<CompanyData> ParseFromStream(StreamReader source)
        {
            var result = new List<CompanyData>();
            var line = source.ReadLine();
            while (!string.IsNullOrEmpty(line))
            {
                var items = ParseLine(line);
                if (items?.Length > 0)
                {
                    result.Add(Interpreter.CreateData(items));
                }

                line = source.ReadLine();
            }

            return result;
        }

        /// <summary>
        /// Interpret a line from input stream to extract required information
        /// </summary>
        /// <param name="line">text containing required data, in specific format</param>
        /// <returns>extracted values</returns>
        protected abstract string[] ParseLine(string line);
    }
}