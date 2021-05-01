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

        public RecursiveFileProcessor(string path, string pattern, string regexpattern = "", Func<FileSystemInfo, bool> filefilter = null, Func<FileSystemInfo, bool> folderfilter = null)
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
            RecursiveFileProcessEnumerator enumerator = new RecursiveFileProcessEnumerator(FileOptions, FolderOptions);
            enumerator.EnterFolderProcess = EnterFolderProcess;
            enumerator.ExitFolderProcess = ExitFolderProcess;

            while (enumerator.MoveNext())
            {
                FileProcess?.Invoke(enumerator.Current);
            }
        }
    }

    public class RecursiveFileProcessEnumerator : FindFilesRecursiveEnumerator
    {
        #region Constructor
        public RecursiveFileProcessEnumerator(FindFilesOptions fileOptions, FindFilesOptions folderOptions) : base(fileOptions, folderOptions)
        { }
        #endregion

        #region Actions
        public Action<FileSystemInfo> EnterFolderProcess { get; set; }
        public Action<FileSystemInfo> ExitFolderProcess { get; set; }
        #endregion

        #region #override methods
        protected override void EnterSubFolder(FileSystemInfo fsi) => EnterFolderProcess?.Invoke(fsi);

        protected override void ExitSubFolder(FileSystemInfo fsi) => ExitFolderProcess?.Invoke(fsi);
        #endregion
    }
}
