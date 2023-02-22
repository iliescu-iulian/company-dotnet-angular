using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DataSource.Core
{
    public class DataBuilder
    {
        private CompanyData _data;
        private string _firstName;
        private string _lastName;

        private readonly Dictionary<DataField, Action<string>> _fieldParsers;

        public DataBuilder()
        {
            _firstName= string.Empty;
            _lastName= string.Empty;
            _data = new CompanyData();
            _fieldParsers = new Dictionary<DataField, Action<string>>
            {
                { DataField.CompanyName, SetCompanyName },
                { DataField.YearFounded, SetYearFounded },
                { DataField.YearsInBusiness, SetYearsInBusiness },
                { DataField.ContactEmail, SetContactEmail },
                { DataField.ContactName, SetContactName },
                { DataField.ContactFirstName, SetContactFirstName },
                { DataField.ContactLastName, SetContactLastName },
                { DataField.ContactPhoneNo, SetContactPhoneNo }
            };
        }
        public DataBuilder SetField(DataField field, string value)
        {
            if (!_fieldParsers.TryGetValue(field, out var fieldSetter))
            {
                throw new ArgumentException($"Unexpected field '{field}'");
            }
            fieldSetter(value);
            return this;
        }

        public CompanyData Build()
        {
            var result = _data;
            _data = new CompanyData();
            return result;
        }

        private void SetCompanyName(string value)
        {
            _data.Name = ValidateString(value, "Company Name");
        }

        private string ValidateString(string value, string label)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException($"{label} cannot be empty");
            }
            return value;
        }

        private int ParseAndValidateNumber(string value, string label, int minVal)
        {
            int num;
            if (!Int32.TryParse(value, out num))
            {
                throw new ArgumentException($"{label} must be a valid number, but is '{value}'");
            }
            if (num < minVal)
            {
                throw new ArgumentOutOfRangeException($"{label} cannot be less than {minVal}, but is '{num}'");
            }
            return num;
        }

        private void SetContactName()
        {
            if (!string.IsNullOrEmpty(_firstName) && !string.IsNullOrEmpty(_lastName))
            {
                _data.Contact.Name = $"{_firstName} {_lastName}";
                _firstName = _lastName = string.Empty;
            }
        }

        private void SetYearFounded(string value)
        {
            _data.Founded = ParseAndValidateNumber(value, "Year Founded", 1500);
        }

        private void SetYearsInBusiness(string value)
        {
            _data.Years = ParseAndValidateNumber(value, "Years in business", 1);
        }

        private void SetContactEmail(string value)
        {
            _data.Contact.Email = ValidateString(value, "Contact Email");
        }

        private void SetContactPhoneNo(string value)
        {
            _data.Contact.Phone = ValidateString(value, "Contact Phone");
        }

        private void SetContactName(string value)
        {
            _data.Contact.Name = ValidateString(value, "Contact Name");
        }

        private void SetContactFirstName(string value)
        {
            _firstName = ValidateString(value, "Contact First Name");
            SetContactName();
        }

        private void SetContactLastName(string value)
        {
            _lastName = ValidateString(value, "Contact Last Name");
            SetContactName();
        }
    }
}
