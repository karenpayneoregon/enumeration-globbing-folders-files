using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryHelpersLibrary.Classes
{
    public class FileOperations
    {
        public static async Task<string> ReadAllTextAsync(string fileName)
        {
            StringBuilder builder = new ();
            
            await using var fileStream = File.OpenRead(fileName);
            
            using var streamReader = new StreamReader(fileStream);
            
            string line = await streamReader.ReadLineAsync();
            
            while (line != null)
            {
                builder.AppendLine(line);
                line = await streamReader.ReadLineAsync();
            }

            return builder.ToString();
        }

        public static async Task<List<string>> ReadAllTextListAsync(string fileName)
        {
            List<string> lineList = new ();
            
            await using var fileStream = File.OpenRead(fileName);
            using var streamReader = new StreamReader(fileStream);

            var line = await streamReader.ReadLineAsync();

            while (line is not null)
            {
                lineList.Add(line);
                line = await streamReader.ReadLineAsync();
            }

            return lineList;
        }
    }
}
