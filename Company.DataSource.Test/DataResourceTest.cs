using Company.DataSource.Core.DataSource;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DataSource.Test
{
    [TestClass]
    public class DataResourceTest
    {
        [DataTestMethod]
        [DataRow("comma", ",", 5)]
        [DataRow("hash", "#", 4)]
        [DataRow("hyphen", "-", 6)]
        public void FromFileLoadConfigContent(string fileName, string delimiter, int fieldCount)
        {
            var expectedPath = Path.Combine(DataResource.DefaultResourcePath, fileName + ".txt");
            var res= DataResource.FromFile(
                Path.Combine(DataResource.DefaultResourcePath, fileName + ".conf"));

            Assert.IsNotNull(res);
            Assert.AreEqual(expectedPath, res.DataFile);
            Assert.AreEqual(delimiter, res.Delimiter);
            Assert.AreEqual(fieldCount, res.Fields.Length);
        }

        [TestMethod]
        public void LoadDataCorrectlyLoadComma()
        {
            var expectedResult = new List<string[]>()
            {
                new[]{"Company A", "John Smith", "(301) 111-1234", "9", "jsmith@company.info" },
                new[]{ "Company Y Corporation", "Jane Jones", "(301) 222-1234", "19", "jjones@company.info" },
                new[]{ "CompanyG LLC.", "James Johnson", "(301) 333-1234", "13", "jjohnson@company.info" }
            };
            var res = DataResource.FromFile(
                Path.Combine(DataResource.DefaultResourcePath, "comma.conf"));

            var ds = res.LoadData();

            Assert.IsInstanceOfType(ds, typeof(TextFileInputSource));
            for(var i= 0; i < 3; ++i)
            {
                var row = ds.Next();
                Assert.IsNotNull(row);
                Assert.AreEqual(5, row.Items.Length);
                for(var j= 0; j < 5; ++j)
                {
                    Assert.AreEqual(expectedResult[i][j], row.Items[j]);
                }
            }
        }
    }
}
