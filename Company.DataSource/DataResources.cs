using System.Collections.Generic;
using System.IO;

namespace Company.DataSource
{
    public class DataResources
    {
        public DataResources() : this(DataResource.DefaultResourcePath) { }
        public DataResources(string resourcePath)
        {
            if (!Directory.Exists(resourcePath))
            {
                throw new DirectoryNotFoundException($"Resource path '{resourcePath}' doesn't exists");
            }
            var resources = new List<DataResource>();
            foreach (var file in Directory.GetFiles(resourcePath, "*.conf"))
            {
                resources.Add(DataResource.FromFile(file));
            }
            Resources = resources.ToArray();
        }

        public DataResource[] Resources { get; }
    }
}
