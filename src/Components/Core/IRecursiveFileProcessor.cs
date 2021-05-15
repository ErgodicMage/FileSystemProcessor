using System;
using System.IO;

namespace FileSystem
{
    public interface IRecursiveFileProcessor
    {
        #region FluentAPI
        IRecursiveFileProcessor WithFileOptions(FindFilesOptions options);
        IRecursiveFileProcessor WithFolderOptions(FindFilesOptions options);
        IRecursiveFileProcessor WithPath(string path);
        IRecursiveFileProcessor WithFilePattern(string pattern);
        IRecursiveFileProcessor WithFolderPattern(string pattern);
        IRecursiveFileProcessor WithFileRegexPattern(string regex);
        IRecursiveFileProcessor WithFolderRegexPattern(string pattern);
        IRecursiveFileProcessor WithFileFilter(Predicate<FileSystemInfo> filter);
        IRecursiveFileProcessor WithFolderFilter(Predicate<FileSystemInfo> filter);
        IRecursiveFileProcessor WithFileAction(Action<FileSystemInfo> action);
        IRecursiveFileProcessor WithEnterFolderAction(Action<FileSystemInfo> action);
        IRecursiveFileProcessor WithExitFolderAction(Action<FileSystemInfo> action);
        #endregion

        #region Properties
        FindFilesOptions FileOptions { get; set; }
        FindFilesOptions FolderOptions { get; set; }

        Action<FileInfo> FileAction { get; set; }
        Action<DirectoryInfo> EnterFolderAction { get; set; }
        Action<DirectoryInfo> ExitFolderAction { get; set; }
        #endregion

        void DoProcess();
    }
}
