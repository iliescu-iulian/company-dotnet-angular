using Company.DataSource.Core;
using Company.DataSource.Core.DataSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DataSource
{
    public class InMemoryResources: IDataSource<CompanyData>
    {
        private List<CompanyData> _data;
        public InMemoryResources() {
            _data = new List<CompanyData>();
            var resources= new DataResources();
            var agr= new InMemoryDataAggregator();
            foreach(var item in resources.Resources)
            {
                var source = item.LoadData();
                agr.AddDataSource(source, item.Fields);
                if(source is IDisposable s)
                {
                    s.Dispose();
                }
            }
            Data = agr.Data;
        }

        public IEnumerable<CompanyData> Data { get; }
    }
}
