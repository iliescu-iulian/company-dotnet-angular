using System.Diagnostics.CodeAnalysis;

namespace Company.DataSource.Core
{
    public class CompanyData : IEquatable<CompanyData>
    {
        private int _yearsInBusiness;
        private int _yearFounded;

        public string Name { get; set; }

        public CompanyData()
        {
            _yearsInBusiness = 0;
            _yearFounded = 0;
            Name= string.Empty;
            Contact = new Contact();
        }

        public CompanyData(string name, Contact contact)
        {
            Name = name;
            Contact = contact;
        }

        public int Years
        {
            get
            {
                return _yearsInBusiness;
            }
            set
            {
                _yearsInBusiness = value;
                _yearFounded = DateTime.Now.Year - value;
            }
        }
        public int Founded
        {
            get
            {
                return _yearFounded;
            }
            set
            {
                _yearFounded = value;
                _yearsInBusiness = DateTime.Now.Year - value;
            }
        }
        public Contact Contact { get; set; }

        public bool Equals(CompanyData? other)
        {
            return Name == other?.Name && Years == other.Years && Contact.Equals(other?.Contact);
        }

        public override bool Equals(object? obj)
        {
            return Equals(this, obj as CompanyData);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() ^ Years.GetHashCode() ^ Contact.GetHashCode();
        }
    }
}
