using System;

namespace Company.DataSource
{
    /// <summary>
    /// Parser specialization
    /// </summary>
    public class CommaParser : DataParser
    {
        /// <inheritdoc />
        protected override string[] ParseLine(string line)
        {
            // Company A, John Smith, (301) 111-1234, 9, jsmith@rapidadvance.info
            return line.Split(',', StringSplitOptions.TrimEntries);
        }
    }
}