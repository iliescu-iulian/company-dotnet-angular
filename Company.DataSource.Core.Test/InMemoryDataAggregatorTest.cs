using Company.DataSource.Core.DataSource;
using Moq;

namespace Company.DataSource.Core.Test
{
    [TestClass]
    public class InMemoryDataAggregatorTest
    {
        [TestMethod]
        public void AddDataSourceCorrectlyMapDataToFields()
        {
            var data = new InMemoryDataAggregator();

            var fields = new DataField[] { DataField.CompanyName, DataField.YearFounded };

            var dataRows = new DataRow[] {
            new DataRow(new string[] { "Test Company", "2005" }),
            new DataRow(new string[] { "Testing Company", "2001" }),
            new DataRow(new string[] { "Test Co.", "1820" }),
            };

            var dataSourceMock = new Mock<IInputSource>();
            dataSourceMock.SetupSequence(x => x.Next())
                .Returns(dataRows[0])
                .Returns(dataRows[1])
                .Returns(dataRows[2]);

            data.AddDataSource(dataSourceMock.Object, fields);

            Assert.AreEqual(dataRows.Length, data.Data.Count);

            for (var i = 0; i < dataRows.Length; ++i)
            {
                Assert.AreEqual(dataRows[i].Items[0], data.Data[i].Name);
                Assert.AreEqual(int.Parse(dataRows[i].Items[1]), data.Data[i].Founded);
            }

        }
    }
}