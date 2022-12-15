using System.Collections.Generic;
using Company.DataSource;

namespace Company.Dto
{
    public class CompanyDataDto
    {
        public CompanyDataDto()
        {}

        public CompanyDataDto(CompanyData source)
        {
            CompanyName = source.Name;
            YearsInBusiness = source.Years;
            ContactName = source.Contact?.Name;
            ContactEmail = source.Contact?.Email;
            ContactPhone = source.Contact?.Phone;
        }

        public static List<CompanyDataDto> Map(IEnumerable<CompanyData> source)
        {
            var result = new List<CompanyDataDto>();

            foreach (var companyData in source)
            {
                result.Add(new CompanyDataDto(companyData));
            }

            return result;
        }
        public string CompanyName { get; set; }
        public int YearsInBusiness { get; set; }

        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
    }
}