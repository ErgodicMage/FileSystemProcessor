using System;
using System.Collections.Generic;
using System.IO;

namespace FileSystem
{
    public class RecursiveFileProcessor
    {
        #region Constructors
        public RecursiveFileProcessor(string path)
        {
            FileOptions = new FindFilesOptions();
            FileOptions.Path = path;
            FileOptions.Recursive = false;
            FolderOptions = new FindFilesOptions();
            FolderOptions.Path = path;
            FolderOptions.Recursive = false;
        }
        public RecursiveFileProcessor(FindFilesOptions fileOptions, FindFilesOptions folderOptions)
        {
            FileOptions = fileOptions;
            FolderOptions = folderOptions;
        }

        public RecursiveFileProcessor(string path, string pattern, string regexpattern = "", Predicate<FileSystemInfo> filefilter = null, Predicate<FileSystemInfo> folderfilter = null)
        {
            FileOptions = new FindFilesOptions() { Path = path, Pattern = pattern, Recursive = false, RegExPattern = regexpattern, Filter = filefilter };
            FolderOptions = new FindFilesOptions();
            FolderOptions.Path = FileOptions.Path;
            FolderOptions.Recursive = false;
            FolderOptions.Filter = folderfilter;
        }
        #endregion

        #region Properties
        public FindFilesOptions FileOptions { get; set; }
        public FindFilesOptions FolderOptions { get; set; }

        public Action<FileSystemInfo> FileProcess { get; set; }
        public Action<FileSystemInfo> EnterFolderProcess { get; set; }
        public Action<FileSystemInfo> ExitFolderProcess { get; set; }
        #endregion

        public void DoProcess()
        {
            FindFilesRecursiveEnumerator enumerator = new FindFilesRecursiveEnumerator(FileOptions, FolderOptions);
            enumerator.EnterFolder = EnterFolderProcess;
            enumerator.ExitFolder = ExitFolderProcess;

            while (enumerator.MoveNext())
            {
                FileProcess?.Invoke(enumerator.Current);
            }
        }
    }
}
