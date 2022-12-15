namespace Company.DataSource
{
    /// <summary>
    /// interpreter definition
    /// </summary>
    public interface IDataInterpreter
    {
        /// <summary>
        /// Creates <see cref="CompanyData"/> based on a list of string values
        /// </summary>
        /// <param name="items"><see cref="CompanyData"/> fields values</param>
        /// <returns><see cref="CompanyData"/> instance created based on input</returns>
        CompanyData CreateData(string[] items);
    }
}