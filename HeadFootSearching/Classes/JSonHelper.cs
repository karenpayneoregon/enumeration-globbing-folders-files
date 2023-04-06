using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DirectoryHelpersLibrary.Classes;
using DirectoryHelpersLibrary.Models;
using Newtonsoft.Json;

namespace HeadFootSearchingStyles.Classes
{
    public static class JSonHelper
    {

        public static async Task<List<FileContainer>> GetAsJson(string path)
        {
            const string fileName = "temp.txt";

            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            var start = new ProcessStartInfo
            {
                FileName = "powershell.exe",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                Arguments = $"Get-ChildItem -Path {path} -Include \"*.cs\" -Exclude \"*ass*.cs,*Designer.cs\" -Recurse -ErrorAction SilentlyContinue  | ConvertTo-Json",
                CreateNoWindow = true
            };

            using var process = Process.Start(start);
            using var reader = process.StandardOutput;

            process.EnableRaisingEvents = true;

            var fileContents = await reader.ReadToEndAsync();

            await File.WriteAllTextAsync(fileName, fileContents);
            await process.WaitForExitAsync();

            var json = await File.ReadAllTextAsync(fileName);

            File.Delete(fileName);

            return JsonConvert.DeserializeObject<List<FileContainer>>(json);

        }

        public static string PowerShellFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PowerShell.txt");
        public static void WriteToFile(List<FileContainer> sender)
        {
            StringBuilder builder = new();
            foreach (var container in sender)
            {
                builder.AppendLine($"{(FileAttributes)container.Attributes,-10}{container.Length.SizeSuffix(),-18}{container.FullName}");
            }

            File.WriteAllText(PowerShellFileName, builder.ToString());
        }




    }

}