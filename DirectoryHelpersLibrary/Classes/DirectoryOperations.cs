using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DirectoryHelpersLibrary.Classes
{
    public class DirectoryOperations
    {
        public delegate void OnException(Exception exception);
        /// <summary>
        /// Callback for subscribers to know about a problem
        /// </summary>
        public static event OnException OnExceptionEvent;

        public delegate void OnEmptyFolderFound(string folder);

        public static event OnEmptyFolderFound EmptyFolderFound;


        /// <summary>
        /// Iterate folder structure to find files with specific extension(s)
        /// </summary>
        /// <param name="path">Path to iterate</param>
        /// <param name="allowedExtensions">one of more file extensions to search</param>
        /// <returns>list of file names or empty list</returns>
        /// <remarks>
        /// * Recommend caller use a try-catch
        /// * Take time to study the code
        /// </remarks>
        public static async Task<List<string>> EnumerateFoldersAsync(string path, string[] allowedExtensions) 
            => await Task.Run(() => Task.FromResult(Directory.EnumerateFiles(path).Where(file => 
                    allowedExtensions.Any(file.ToLower().EndsWith)).ToList()));

        /// <summary>
        /// Iterate folder structure to find files with specific extension(s)
        /// </summary>
        /// <param name="path">Path to iterate</param>
        /// <param name="allowedExtensions">one of more file extensions to search</param>
        /// <returns>true/list of file names or false if there was an exception</returns>
        /// <remarks>
        /// * Recommend caller use a try-catch
        /// * Take time to study the code
        /// </remarks>
        public static async Task<(bool success, List<string> list)> EnumerateFoldersAsync1(string path, string[] allowedExtensions)
        {
            try
            {
                var list = await Task.Run(() => Task.FromResult(
                    Directory.EnumerateFiles(path).Where(file => 
                        allowedExtensions.Any(file.ToLower().EndsWith)).ToList()));

                return (true, list);
            }
            catch (Exception exception)
            {
                OnExceptionEvent?.Invoke(exception);
                return (false, new List<string>());
            }
        }
        public static async Task<List<string>> EnumerateFoldersAsync(string path)
            => await Task.Run(() 
                => Task.FromResult(Directory.EnumerateFiles(path).ToList()));

        /// <summary>
        /// Demo to show how to find empty folders
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static async Task FindEmptyFolders(string path)
        {
            await Task.Run(async () =>
            {
                await Task.Delay(1);

                foreach (var directory in Directory.GetDirectories(path))
                {
                    _ = FindEmptyFolders(directory);
                    if (Directory.GetFiles(directory).Length == 0 && Directory.GetDirectories(directory).Length == 0)
                    {
                        EmptyFolderFound?.Invoke(directory);
                    }
                }

            });
        }
    }

}
