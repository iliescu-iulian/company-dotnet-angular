using Company.DataSource.Core;
using Company.DataSource.Core.DataSource;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;

namespace Company.DataSource
{
    public class DataResource
    {
        public static string DefaultResourcePath { get; }
        class ConfigData
        {
            public string File { get; set; }
            public string Delimiter { get; set; }
            public string[] Fields { get; set; }
        }

        static DataResource()
        {
            DefaultResourcePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Resources");
        }

        DataResource(ConfigData data, string hintPath)
        {
            if(data.Fields.Length == 0)
            {
                throw new ArgumentException("At least one field is required for config");
            }
            if(File.Exists(data.File))
            {
                DataFile = data.File;
            }
            else
            {
                var hintFile = Path.Combine(
                    string.IsNullOrEmpty(hintPath) ? DefaultResourcePath : hintPath, data.File);
                if (File.Exists(hintFile))
                {
                    DataFile = hintFile;
                }
                else
                {
                    throw new ArgumentException($"Data file '{data.File}' cannot be found.");
                }
            }
            var ext = Path.GetExtension(data.File).ToLower();
            switch (ext)
            {
                case ".txt":
                    Type= DataType.Csv; break;
                default:
                    throw new ArgumentException($"Unknown file extension '{ext}'");


            }
            Delimiter = data.Delimiter;
            var fields= new List<DataField>();
            foreach(var field in data.Fields)
            {
                if (!Enum.TryParse(field, true, out DataField f))
                {
                    throw new ArgumentException($"DataField '{field}' is not recognized.");
                }
                fields.Add(f);
            }
            Fields = fields.ToArray();
        }

        public static DataResource FromFile(string configFile)
        {
            if (!File.Exists(configFile))
            {
                throw new FileNotFoundException($"Data config file '{configFile}' doesn't exists");
            }
            var json=File.ReadAllText(configFile);
            return new DataResource(DeserializeData(json), Path.GetDirectoryName(configFile));
        }

        public static DataResource FromString(string json)
        {
            return new DataResource(DeserializeData(json), null);
        }

        static ConfigData DeserializeData(string json)
        {
            try
            {
               return JsonSerializer.Deserialize<ConfigData>(json);
            }
            catch(JsonException e)
            {
                throw new FormatException("Invalid JSON format", e);
            }
        }

        public IInputSource LoadData()
        {
            if(Type == DataType.Csv)
            {
                return new TextFileInputSource(DataFile, Delimiter);
            }
            throw new InvalidDataException($"Unexpected data type '{Type}'");
        }

        public string DataFile { get; private set; }
        public string Delimiter { get; private set; }
        public DataField[] Fields { get; private set; }
        public DataType Type { get; private set; }
    }
}
