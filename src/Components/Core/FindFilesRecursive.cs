using System;
using System.Collections.Generic;
using System.IO;

namespace FileSystem
{
    public class FindFilesRecursive : IFindFileSystem
    {
        #region Constructors
        public FindFilesRecursive(string path)
        {
            Options = new FindFilesOptions();
            Options.Path = path;
            Options.Recursive = false;
            FolderOptions = new FindFilesOptions();
            FolderOptions.Path = path;
            FolderOptions.Recursive = false;
        }

        public FindFilesRecursive(FindFilesOptions options)
        {
            Options = options;
            Options.Recursive = false;
            FolderOptions = new FindFilesOptions();
            FolderOptions.Path = Options.Path;
            FolderOptions.Recursive = false;
        }

        public FindFilesRecursive(string path, string pattern, string regexpattern = "", Func<FileSystemInfo, bool> filter = null)
        {
            Options = new FindFilesOptions() { Path = path, Pattern = pattern, Recursive=false, RegExPattern = regexpattern, Filter = filter };
            FolderOptions = new FindFilesOptions();
            FolderOptions.Path = Options.Path;
            FolderOptions.Recursive = false;
        }
        #endregion

        #region Properties
        public FindFilesOptions Options { get; set; }
        public FindFilesOptions FolderOptions { get; protected set; }
        #endregion

        #region Functions
        public IEnumerable<FileSystemInfo> Enumerate() => new FindFilesRecursiveEnumerable(Options, FolderOptions);
        #endregion
    }
}
