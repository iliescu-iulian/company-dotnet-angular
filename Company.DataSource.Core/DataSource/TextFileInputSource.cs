using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DataSource.Core.DataSource
{
    public class TextFileInputSource : IInputSource, IDisposable
    {
        StreamReader? _file;
        string _delimiter;
        public TextFileInputSource(string path, string delimiter)
        {
            _file = new StreamReader(path);
            if (string.IsNullOrEmpty(delimiter))
            {
                throw new ArgumentException("Delimiter cannot be empty");
            }
            _delimiter = delimiter;
        }

        public void Dispose()
        {
            if (_file != null)
            {
                _file.Close();
                _file = null;
            }
        }

        public DataRow? Next()
        {
            if (_file == null)
            {
                return null;
            }

            // skip empty lines
            string? line = _file.ReadLine();
            while (line != null)
            {
                if (line.Length > 0)
                {
                    return new DataRow(line.Split(_delimiter, StringSplitOptions.TrimEntries));
                }
                line = _file.ReadLine();
            }

            return null;
        }
    }
}
