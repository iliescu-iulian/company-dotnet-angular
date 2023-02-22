using Company.DataSource.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DataSource.Test
{
    [TestClass]
    public class InMemoryResourcesTest
    {
        [TestMethod]
        public void AllResourcesAreLoadedInConstructor()
        {
            var res= new InMemoryResources();
            Assert.AreEqual(9, res.Data.Count());

            foreach(var data in res.Data)
            {
                Assert.IsFalse(string.IsNullOrWhiteSpace(data.Name));
                Assert.IsTrue(data.Years > 1);
                Assert.IsTrue(data.Founded >= 1900, $"Invalid Founded {data.Founded} for {data.Name}");
                Assert.IsFalse(string.IsNullOrWhiteSpace(data.Contact.Name), $"Contact name missing for {data.Name}");
                Assert.IsFalse(string.IsNullOrWhiteSpace(data.Contact.Phone), $"Phone missing for {data.Name}");
            }
        }

        [TestMethod]
        public void ResourcesLoadedAsExpected()
        {
            var res= new InMemoryResources();
            Assert.AreEqual(9, res.Data.Count());

            var expectedData = new List<CompanyData>()
            {
                new CompanyData("Company A", new Contact("John Smith", "(301) 111-1234", "jsmith@company.info")) {Years= 9},
                new CompanyData("Company JK", new Contact("Jimmy Green", "+1 (301) 667-1234", string.Empty)) {Founded= 1900},
                new CompanyData("Creative Name Corp.", new Contact("Bob Stone", "+13016674477", "bstone@company.info")) {Founded= 2000},
            };

            AssertCompanyData(expectedData[0], res.Data.ElementAt(0));
            AssertCompanyData(expectedData[1], res.Data.ElementAt(3));
            AssertCompanyData(expectedData[2], res.Data.ElementAt(6));
        }

        private void AssertCompanyData(CompanyData lhs, CompanyData rhs)
        {
            Assert.AreEqual(lhs.Name, rhs.Name);
            Assert.AreEqual(lhs.Years, rhs.Years);
            Assert.AreEqual(lhs.Contact, rhs.Contact);
        }
    }
}
