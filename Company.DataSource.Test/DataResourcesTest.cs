using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;

namespace Company.DataSource.Test
{
    [TestClass]
    public class DataResourcesTest
    {
        [TestMethod]
        public void DefaultContructorLoadsResources()
        {
            var data= new DataResources();
            Assert.IsNotNull(data);
            Assert.AreEqual(3, data.Resources.Length);

            foreach(var res in data.Resources)
            {
                Assert.IsTrue(res.Fields.Length > 1);
                Assert.IsTrue(File.Exists(res.DataFile));
                Assert.IsFalse(string.IsNullOrWhiteSpace(res.Delimiter));
                Assert.AreEqual(DataType.Csv, res.Type);
            }
        }
    }
}
