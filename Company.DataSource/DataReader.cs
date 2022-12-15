using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Company.DataSource
{
    /// <summary>
    /// data reader implementation that reads data from text files
    /// </summary>
    public class DataReader : IDataReader
    {
        private Dictionary<string, DataType> DataTypeMap = new Dictionary<string, DataType>()
        {
            { "comma.txt", DataType.Comma },
            { "hash.txt", DataType.Hash },
            { "hyphen.txt", DataType.Hyphen }
        };

        private List<CompanyData> LoadFileContent(string path)
        {
            var fileName = Path.GetFileName(path);
            var type = DataTypeMap[fileName];
            var parser = ParserFactory.Create(type);

            using var s = new StreamReader(path);
            return parser.ParseFromStream(s);
        }

        /// <inheritdoc />
        public IList<CompanyData> ReadAll()
        {
            var result = new List<CompanyData>();
            var sourceDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var files = Directory.GetFiles(Path.Combine(sourceDir, "Resources"));
            foreach (var file in files)
            {
                result.AddRange(LoadFileContent(file));
            }

            return result;
        }
    }
}