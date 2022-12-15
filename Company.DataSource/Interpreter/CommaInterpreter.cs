namespace Company.DataSource
{
    /// <summary>
    /// Interpreter specialization
    /// </summary>
    public class CommaInterpreter : IDataInterpreter
    {
        /// <summary>
        /// creates <see cref="CompanyData"/> based on a list of strings expected in a certain order
        /// </summary>
        /// <param name="items">field values, expected as (Company Name, Contact Name, Contact Phone Number, Years in business, Contact Email)</param>
        /// <returns>instance of <see cref="CompanyData"/></returns>
        /// <exception cref="DataError">cannot created object based on input</exception>
        public CompanyData CreateData(string[] items)
        {
            // Company Name, Contact Name, Contact Phone Number, Years in business, Contact Email
            if (items.Length != 5)
            {
                throw new DataError(DataError.InvalidData);
            }
            return new CompanyData
            {
                Name = items[0],
                Years = int.Parse(items[3]),
                Contact = new Contact
                {
                    Name = items[1],
                    Phone = items[2],
                    Email = items[4]
                }
            };
        }
    }
}