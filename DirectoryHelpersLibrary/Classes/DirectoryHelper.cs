using System;
using System.Collections.Generic;
using System.IO;

namespace DirectoryHelpersLibrary.Classes
{
    /// <summary>
    /// Provides code to iterate a folder forwards or backwards, see SolutionFolder()
    /// for how to use which when executed from a project in a Visual Studio solution
    /// will provide the solution folder path.
    /// </summary>
    public static class DirectoryHelper
    {
        public static string UpperFolder(this string folderName, int level)
        {
            var folderList = new List<string>();

            while (!string.IsNullOrWhiteSpace(folderName))
            {
                var parentFolder = Directory.GetParent(folderName);
                if (parentFolder == null) break;
                folderName = Directory.GetParent(folderName)?.FullName;
                folderList.Add(folderName);
            }

            return folderList.Count > 0 && level > 0 ? level - 1 <= folderList.Count - 1 ? folderList[level - 1] : folderName : folderName;
        }

        /// <summary>
        /// From project folder, get the solution folder path
        /// </summary>
        /// <returns>folder name</returns>
        public static string SolutionFolder()
            => AppDomain.CurrentDomain.BaseDirectory.UpperFolder(5);
    }
}