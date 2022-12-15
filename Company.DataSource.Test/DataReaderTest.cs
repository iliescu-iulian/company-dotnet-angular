using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Company.DataSource.Test
{
    [TestClass]
    public class DataReaderTest
    {
        [TestMethod]
        public void ReadAllSuccess()
        {
            var data= new DataReader().ReadAll();
            Assert.IsNotNull(data);
            Assert.AreEqual(data.Count, 9);
        }
    }
}
