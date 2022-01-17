using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryHelpersLibrary.Classes
{
    public class DirectoryOperations2
    {
        public delegate void OnTraverse(string sender);
        public static event OnTraverse Traverse;

        public delegate void OnTraverseFolder(string sender);
        public static event OnTraverseFolder TraverseFolder;

        public delegate void OnDone();
        public static event OnDone Done;

        public delegate void OnDoneFiles();
        public static event  OnDoneFiles DoneFiles;


        public delegate void OnFolderException(string sender);
        public static event OnFolderException FolderException;

        public static async Task Example1Async(string path, string searchPattern, SearchOption searchOption = SearchOption.AllDirectories)
        {

            using var enumerator = await Task.Run(() => Directory.EnumerateFiles(path, searchPattern, searchOption).GetEnumerator());

            while (await Task.Run(() => enumerator.MoveNext()))
            {
                Traverse?.Invoke(enumerator.Current);
            }

            Done?.Invoke();

        }


        public static async Task Example2Async(string path, string searchPattern, SearchOption searchOption = SearchOption.AllDirectories)
        {

            using var enumerator = await Task.Run(() => Directory.EnumerateFiles(path, searchPattern, searchOption).GetEnumerator());

            while (await Task.Run(() => enumerator.MoveNext()))
            {
                Traverse?.Invoke(enumerator.Current);
            }

            DoneFiles?.Invoke();

        }
        /// <summary>
        /// Iterate file with a Func&lt;string, Task&gt;
        /// </summary>
        /// <param name="path">Path to iterate</param>
        /// <param name="searchPattern">Pattern-filter</param>
        /// <param name="searchOption"><see cref="SearchOption"/></param>
        /// <param name="workAsync">Perform work on each file</param>
        public static async Task Example3Async(string path, string searchPattern, SearchOption searchOption, Func<string, Task> workAsync)
        {
            using var enumerator = await Task.Run(() => Directory.EnumerateFiles(path, searchPattern, searchOption).GetEnumerator());

            while (await Task.Run(() => enumerator.MoveNext()))
            {
                await workAsync(enumerator.Current);
            }

            DoneFiles?.Invoke();
        }

        public static void Example1Sync(string path, string searchPattern, SearchOption searchOption = SearchOption.AllDirectories)
        {
            var filePaths = Directory.EnumerateFiles(path, searchPattern, searchOption);
            foreach (var file in filePaths)
            {
                Traverse?.Invoke(file);
            }

            Done?.Invoke();
        }
        public static void Example2Sync(string path, string searchPattern, EnumerationOptions options, SearchOption searchOption = SearchOption.AllDirectories )
        {
            
            var filePaths = Directory.EnumerateFiles(path, searchPattern, searchOption);
            foreach (var file in filePaths)
            {
                Traverse?.Invoke(file);
            }

            Done?.Invoke();
        }

        public static int _numberOfFolders { get; set; }
        private static readonly ConcurrentBag<Task> _concurrentBagTasks = new();

        // taken from the following and modified for this article
        // https://stackoverflow.com/questions/34579606/asynchronously-enumerate-folders
        public static async Task CollectFolders(string path)
        {

            await Task.Delay(1);

            DirectoryInfo directoryInfo = new(path);
            _concurrentBagTasks.Add(Task.Run(() => CrawlFolder(directoryInfo)));

            while (_concurrentBagTasks.TryTake(out var taskToWaitFor))
            {
                taskToWaitFor.Wait();
            }

            Done?.Invoke();
        }


        private static void CrawlFolder(DirectoryInfo dir)
        {
            try
            {
                DirectoryInfo[] directoryInfos = dir.GetDirectories();
                foreach (DirectoryInfo childInfo in directoryInfos)
                {
                    DirectoryInfo di = childInfo;
                    _concurrentBagTasks.Add(Task.Run(() => CrawlFolder(di)));
                }

                TraverseFolder?.Invoke($"{dir.FullName}");

                _numberOfFolders++;

            }
            catch (Exception ex)
            {
                StringBuilder builder = new();

                while (ex != null)
                {
                    builder.AppendLine($"{ex.GetType()} {ex.Message}\n{ex.StackTrace}");
                    ex = ex.InnerException;
                }

                FolderException?.Invoke(builder.ToString());
            }
        }
    }
}
