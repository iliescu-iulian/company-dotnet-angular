using System.Collections.Generic;

namespace Company.DataSource
{
    /// <summary>
    /// Definition of data reader
    /// </summary>
    public interface IDataReader
    {
        /// <summary>
        /// Read all available data
        /// </summary>
        /// <returns>data as a list of <see cref="CompanyData"/></returns>
        IList<CompanyData> ReadAll();
    }
}