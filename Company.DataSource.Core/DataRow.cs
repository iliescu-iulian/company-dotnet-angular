using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DataSource.Core
{
    public class DataRow
    {
        string[] _items;

        public DataRow(string[] items)
        {
            _items = items;
        }

        public string[] Items { get { return _items; } }
    }
}
