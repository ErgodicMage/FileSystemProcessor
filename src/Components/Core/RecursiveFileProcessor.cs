namespace ErgodicMage.FileSystemProcessor;

public class RecursiveFileProcessor : IRecursiveFileProcessor
{
    #region Constructors
    public RecursiveFileProcessor()
    {
        FileOptions = new FindFilesOptions();
        FolderOptions = new FindFilesOptions();
    }
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

    #region FluentAPI
    public IRecursiveFileProcessor WithFileOptions(FindFilesOptions options)
    {
        FileOptions = options;
        return this;
    }

    public IRecursiveFileProcessor WithFolderOptions(FindFilesOptions options)
    {
        FolderOptions = FileOptions;
        return this;
    }

    public IRecursiveFileProcessor WithPath(string path)
    {
        FolderOptions.Path = path;
        return this;
    }

    public IRecursiveFileProcessor WithFilePattern(string pattern)
    {
        FileOptions.Pattern = pattern;
        return this;
    }

    public IRecursiveFileProcessor WithFolderPattern(string pattern)
    {
        FolderOptions.Pattern = pattern;
        return this;
    }

    public IRecursiveFileProcessor WithFileRegexPattern(string regex)
    {
        FileOptions.RegExPattern = regex;
        return this;
    }

    public IRecursiveFileProcessor WithFolderRegexPattern(string regex)
    {
        FolderOptions.RegExPattern = regex;
        return this;
    }

    public IRecursiveFileProcessor WithFileFilter(Predicate<FileSystemInfo> filter)
    {
        FileOptions.Filter = filter;
        return this;
    }

    public IRecursiveFileProcessor WithFolderFilter(Predicate<FileSystemInfo> filter)
    {
        FolderOptions.Filter = filter;
        return this;
    }

    public IRecursiveFileProcessor WithFileAction(Action<FileInfo> action)
    {
        FileAction = action;
        return this;
    }

    public IRecursiveFileProcessor WithEnterFolderAction(Action<DirectoryInfo> action)
    {
        EnterFolderAction = action;
        return this;
    }

    public IRecursiveFileProcessor WithExitFolderAction(Action<DirectoryInfo> action)
    {
        ExitFolderAction = action;
        return this;
    }
    #endregion

    #region Properties
    public FindFilesOptions FileOptions { get; set; }
    public FindFilesOptions FolderOptions { get; set; }

    public Action<FileInfo> FileAction { get; set; }
    public Action<DirectoryInfo> EnterFolderAction { get; set; }
    public Action<DirectoryInfo> ExitFolderAction { get; set; }
    #endregion

    public void DoProcess()
    {
        FindFilesRecursiveEnumerator enumerator = new FindFilesRecursiveEnumerator(FileOptions, FolderOptions);
        enumerator.EnterFolder = EnterFolderAction;
        enumerator.ExitFolder = ExitFolderAction;

        while (enumerator.MoveNext())
        {
            FileAction?.Invoke(enumerator.Current as FileInfo);
        }
    }
}
