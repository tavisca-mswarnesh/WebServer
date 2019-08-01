using System;
using System.IO;

namespace ExampleSimpleWebserver
{
    class FileParser
    {
        private string _fileName;
        public FileParser(string fileName)
        {
            _fileName = fileName;
        }
        public byte[] Parse()
        {
            FileStream fileStream = new FileStream(_fileName, FileMode.Open, FileAccess.Read);
            byte[] buffer = File.ReadAllBytes(_fileName);
            fileStream.Read(buffer, 0, Convert.ToInt32(fileStream.Length));
            return buffer;
        }
    }
    
}
