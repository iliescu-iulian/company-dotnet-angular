using System;

namespace Company.DataSource
{
    /// <summary>
    /// Interpreter specialization
    /// </summary>
    public class HyphenInterpreter : IDataInterpreter
    {
        /// <summary>
        /// creates <see cref="CompanyData"/> based on a list of strings expected in a certain order
        /// </summary>
        /// <param name="items">field values, expected as (Company Name, Year Founded, Contact Phone Number, Contact Email, Contact First Name, Contact Last Name)</param>
        /// <returns>instance of <see cref="CompanyData"/></returns>
        /// <exception cref="DataError">cannot created object based on input</exception>
        public CompanyData CreateData(string[] items)
        {
            // Company Name, Year Founded, Contact Phone Number, Contact Email, Contact First Name, Contact Last Name
            if (items.Length != 6)
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
                    Name = $"{items[4]} {items[5]}",
                    Phone = items[2],
                    Email = items[3]
                }
            };
        }
    }
}