using System;

namespace Company.DataSource
{
    /// <summary>
    /// Parser specialization
    /// </summary>
    public class HyphenParser : DataParser
    {
        /// <inheritdoc />
        protected override string[] ParseLine(string line)
        {
            // Creative Name Corp.-2000-+13016674477-bstone@rapidadvance.info-Bob-Stone
            return line.Split('-', StringSplitOptions.TrimEntries);
        }
    }
}