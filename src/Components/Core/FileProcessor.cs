namespace FileSystem;

public class FileProcessor : IFileSystemProcessor
{
    #region Constructors
    public FileProcessor()
    {
        Options = new FindFilesOptions();
    }

    public FileProcessor(string path)
    {
        Options = new FindFilesOptions();
    }

    public FileProcessor(FindFilesOptions options)
    {
        Options = options;
    }

    public FileProcessor(string path, string pattern, bool recursive, string regexpattern = "", Predicate<FileSystemInfo> filter = null)
    {
        Options = new FindFilesOptions() { Path = path, Pattern = pattern, RegExPattern = regexpattern, Filter = filter, Recursive = recursive };
    }
    #endregion

    #region Fluent API
    public IFileSystemProcessor WithOptions(FindFilesOptions options)
    {
        Options = options;
        return this;
    }

    public IFileSystemProcessor WithPath(string path)
    {
        Options.Path = path;
        return this;
    }

    public IFileSystemProcessor WithPattern(string pattern)
    {
        Options.Pattern = pattern;
        return this;
    }

    public IFileSystemProcessor WithRegexPattern(string regex)
    {
        Options.RegExPattern = regex;
        return this;
    }

    public IFileSystemProcessor Recursive()
    {
        Options.Recursive = true;
        return this;
    }

    public IFileSystemProcessor WithFilter(Predicate<FileSystemInfo> filter)
    {
        Options.Filter = filter;
        return this;
    }

    public IFileSystemProcessor WithAction(Action<FileSystemInfo> action)
    {
        Action = action;
        return this;
    }
    #endregion

    #region Properties
    public FindFilesOptions Options { get; set; }

    public Action<FileSystemInfo> Action { get; set; }
    #endregion

    #region Methods
    public virtual void DoProcess()
    {
        FindFiles ff = new FindFiles(Options);

        foreach (FileSystemInfo fsi in ff.Enumerate())
        {
            // note this is assuming the action will handle any exception!
            Action?.Invoke(fsi);
        }
    }
    #endregion
}
