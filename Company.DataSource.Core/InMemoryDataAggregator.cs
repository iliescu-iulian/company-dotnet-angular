using Company.DataSource.Core.DataSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DataSource.Core
{
    public class InMemoryDataAggregator
    {
        private List<CompanyData> _data;
        public InMemoryDataAggregator()
        {
            _data = new List<CompanyData>();
        }

        public IList<CompanyData> Data => _data;

        public void AddDataSource(IInputSource dataSource, DataField[] fields)
        {
            if (dataSource == null)
            {
                throw new ArgumentNullException(
                nameof(dataSource));
            }
            if (fields == null)
            {
                throw new ArgumentNullException(nameof(fields));
            }
            if (fields.Length == 0)
            {
                throw new ArgumentException("At least one data field is required");
            }

            var row = dataSource.Next();
            while (row != null)
            {
                if (row.Items.Length != fields.Length)
                {
                    throw new FormatException($"Expected #{fields.Length} items on a data row, but have #{row.Items.Length}");
                }

                var builder = new DataBuilder();

                for (var i = 0; i < fields.Length; ++i)
                {
                    builder.SetField(fields[i], row.Items[i]);
                }

                _data.Add(builder.Build());
                row = dataSource.Next();
            }
        }
    }
}
