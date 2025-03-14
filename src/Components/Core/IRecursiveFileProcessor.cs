namespace ErgodicMage.FileSystemProcessor;

public interface IRecursiveFileProcessor : IProcessor
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
    IRecursiveFileProcessor WithFileAction(Action<FileInfo?> action);
    IRecursiveFileProcessor WithEnterFolderAction(Action<DirectoryInfo> action);
    IRecursiveFileProcessor WithExitFolderAction(Action<DirectoryInfo> action);
    #endregion

    #region Properties
    FindFilesOptions FileOptions { get; set; }
    FindFilesOptions FolderOptions { get; set; }

    Action<FileInfo>? FileAction { get; set; }
    Action<DirectoryInfo>? EnterFolderAction { get; set; }
    Action<DirectoryInfo>? ExitFolderAction { get; set; }
    #endregion
}
