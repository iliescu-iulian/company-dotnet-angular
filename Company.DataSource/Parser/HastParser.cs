using System;

namespace Company.DataSource
{
    /// <summary>
    /// Parser specialization
    /// </summary>
    public class HastParser : DataParser
    {
        /// <inheritdoc />
        protected override string[] ParseLine(string line)
        {
            // Company JK#1900#Jimmy Green#+1 (301) 667-1234
            return line.Split('#', StringSplitOptions.TrimEntries);
        }
    }
}