using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.Json;

namespace Company.DataSource.Test
{
    [TestClass]
    public class JsonTest
    {
        class Sample
        {
            public int Value { get; set; }
            public string Text { get; set; }
        }
        [TestMethod]
        public void TestJson()
        {
            var s = new Sample { Value = 21, Text = "Demo" };
            var jsonStr = JsonSerializer.Serialize(s);

            Assert.IsNotNull(jsonStr);
            StringAssert.Contains(jsonStr, "{\"Value\":21,\"Text\":\"Demo\"}");

            var s2= JsonSerializer.Deserialize<Sample>(jsonStr);
            Assert.IsNotNull(s2);
            Assert.AreEqual(s.Text, s2.Text);
            Assert.AreEqual(s.Value, s2.Value);
        }

        [TestMethod]
        public void TestJsonDeserializer()
        {
            var expected = new Sample { Value = 21, Text = "Demo" };
            var jsonStr = "{ \"Value\": 21, \"Text\": \"Demo\" }";
            var actual = JsonSerializer.Deserialize<Sample>(jsonStr);
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Text, actual.Text);
            Assert.AreEqual(expected.Value, actual.Value);
        }

        [TestMethod]
        public void TestJsonDeserializeEmptyJson()
        {
            var jsonStr = "{  }";
            var actual = JsonSerializer.Deserialize<Sample>(jsonStr);
            Assert.IsNotNull(actual);
            Assert.IsNull(actual.Text);
            Assert.AreEqual(0, actual.Value);
        }

        [TestMethod]
        public void TestJsonDeserializeInvalidJsonThrows() {

            Assert.ThrowsException<System.Text.Json.JsonException>(() =>
            JsonSerializer.Deserialize<Sample>("not a json"));
        }
    }
}
