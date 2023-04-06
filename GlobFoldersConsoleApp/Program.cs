using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GlobExpressions;
using GlobFoldersConsoleApp.Classes;

namespace GlobFoldersConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var solutionFolder = DirectoryHelper.SolutionFolder();
            var folderWithManyProjects = @"C:\OED\Dotnetland\VS2019\";

            string[] folderPatterns = { "**/bin", "**/obj" };
            string[] filePatterns = { "**/Program.cs"};

            List<string> folderResults = new();
            List<string> fileResults = new();

            foreach (var pattern in folderPatterns)
            {
                folderResults.AddRange(Glob.Directories(solutionFolder, pattern).ToArray());
            }

            if (Directory.Exists(folderWithManyProjects))
            {
                foreach (var pattern in filePatterns)
                {
                    fileResults.AddRange(Glob.Files(@folderWithManyProjects, pattern).ToArray());
                }

                Console.WriteLine($"File count {fileResults.Count}");
            }


            Console.WriteLine($"Folder count {folderResults.Count}");
            
            Console.ReadLine();
        }
    }
}
