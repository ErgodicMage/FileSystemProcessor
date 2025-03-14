namespace ErgodicMage.FileSystemProcessor;

public interface IFileSystemProcessor : IProcessor
{
    #region Fluent API
    IFileSystemProcessor WithOptions(FindFilesOptions options);
    IFileSystemProcessor WithPath(string path);
    IFileSystemProcessor WithPattern(string pattern);
    IFileSystemProcessor WithRegexPattern(string regex);
    IFileSystemProcessor Recursive();
    IFileSystemProcessor WithFilter(Predicate<FileSystemInfo> filter);
    IFileSystemProcessor WithAction(Action<FileSystemInfo> action);
    #endregion

    FindFilesOptions Options { get; set; }
    Action<FileSystemInfo>? Action { get; set; }

}
