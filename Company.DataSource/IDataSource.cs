using Company.DataSource.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DataSource
{
    public interface IDataSource<T>
    {
        IEnumerable<T> Data { get; }
    }
}
