using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DataSource.Core
{
    public class Contact: IEquatable<Contact>
    {
        public string Name { get; set; }

        public Contact()
            : this(string.Empty, string.Empty, string.Empty) { }

        public Contact(string name, string phone, string email)
        {
            Name = name;
            Phone = phone;
            Email = email;
        }

        public string Phone { get; set; }
        public string Email { get; set; }

        public bool Equals(Contact? other)
        {
            try
            {
                return Name == other?.Name && this?.Email == other?.Email && Phone == other?.Phone;
            }
            catch
            {
                return false;
            }
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Contact);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() ^ Phone.GetHashCode() ^ Email.GetHashCode();
        }
    }
}
