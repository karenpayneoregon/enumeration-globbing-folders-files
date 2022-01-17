﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DirectoryHelpersLibrary.Extensions
{
    /// <summary>
    /// https://www.techmikael.com/2010/02/directory-search-with-multiple-filters.html
    /// One version takes a regex pattern \.mp3|\.mp4, and the other a string list and runs in parallel.
    /// GetFiles
    /// </summary>
    public static class DirectoryExtensions
    {
        // Regex version
        public static IEnumerable<string> GetFiles(string path,
            string searchPatternExpression = "",
            SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {

            Regex reSearchPattern = new Regex(searchPatternExpression, RegexOptions.IgnoreCase);

            return Directory.EnumerateFiles(path, "*", searchOption)
                .Where(file =>
                    reSearchPattern.IsMatch(Path.GetExtension(file)));
        }

        // Takes same patterns, and executes in parallel
        public static IEnumerable<string> GetFiles(string path,
            string[] searchPatterns,
            SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            return searchPatterns.AsParallel()
                .SelectMany(searchPattern =>
                    Directory.EnumerateFiles(path, searchPattern, searchOption));
        }

        public static IEnumerable<FileInfo> GetFilesByExtensions(this DirectoryInfo dir, params string[] extensions)
        {
            if (extensions == null)
            {
                throw new ArgumentNullException("extensions");
            }

            IEnumerable<FileInfo> files = dir.EnumerateFiles();
            return files.Where((fi) => extensions.Contains(fi.Extension));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path">Folder</param>
        /// <param name="originalExtension">File extension to change</param>
        /// <param name="replacementExtension">Replacement extension</param>
        /// <returns>success true or false, if false the exception raised</returns>
        public static (bool Success, Exception Exception) RenameExtensions(string path, string originalExtension, string replacementExtension)
        {
            try
            {
                new DirectoryInfo(path).GetFiles($"*.{originalExtension}")
                    .ToList()
                    .ForEach(currentFile =>
                    {
                        var filename = Path.ChangeExtension(currentFile.Name, $".{replacementExtension}");

                        var tempName = Path.Combine(path, filename!);

                        if (File.Exists(tempName))
                        {
                            File.Delete(tempName);
                        }

                        File.Move(currentFile.Name!, filename);

                    });

                return (true, null);
            }
            catch (Exception exception)
            {
                return (false, exception);
            }
        }
    }
}
