using System;

namespace Company.DataSource
{
    /// <summary>
    /// Interpreter specialization
    /// </summary>
    public class HashInterpreter : IDataInterpreter
    {
        /// <summary>
        /// creates <see cref="CompanyData"/> based on a list of strings expected in a certain order
        /// </summary>
        /// <param name="items">field values, expected as (Company Name, Year Founded, Contact Name, Contact Phone Number)</param>
        /// <returns>instance of <see cref="CompanyData"/></returns>
        /// <exception cref="DataError">cannot created object based on input</exception>
        public CompanyData CreateData(string[] items)
        {
            // Company Name, Year Founded, Contact Name, Contact Phone Number
            if (items.Length != 4)
            {
                throw new DataError(DataError.InvalidData);
            }
            var currentYear = DateTime.Now.Year;
            return new CompanyData
            {
                Name = items[0],
                Years = currentYear - int.Parse(items[1]),
                Contact = new Contact
                {
                    Name = items[2],
                    Phone = items[3]
                }
            };
        }
    }
}